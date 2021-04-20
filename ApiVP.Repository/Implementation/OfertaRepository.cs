using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class OfertaRepository : IOfertaRepository
    {
        private readonly ApplicationDbContext context;

        public OfertaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var ofertaId = await context.Ofertas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (ofertaId == default(int))
            {
                return false;
            }
            context.Ofertas.Remove(new Oferta { Id = ofertaId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Oferta> Get(int id)
        {
            var oferta = await context.Ofertas.Include(x => x.Anciano).ThenInclude(x => x.AncianoABVCs).ThenInclude(x => x.ABVC)
                                                .Include(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            var anciano = await context.Ancianos.Where(x => x.Id == oferta.AncianoId).Include(x => x.Persona).FirstOrDefaultAsync();
            oferta.Anciano = anciano;

            return oferta;
        }

        public async Task<List<Oferta>> GetAll()
        {
            var ofertas = await context.Ofertas.Where(x => x.Estado == "activo").Include(x => x.Anciano).ThenInclude(x => x.Persona)
                                                .Include(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora)
                                                .ToListAsync();
            return ofertas;
        }

        public async Task<List<Oferta>> GetOfertasByCorreo(string param)
        {
            var familiar = await context.Familiares.Where(x => x.Usuario.Correo == param).FirstOrDefaultAsync();
            if (familiar == null)
            {
                return null;
            }
            var ofertas = await context.Ofertas.Where(x => x.Anciano.FamiliarId == familiar.Id).Where(x => x.Estado == "activo")
                                                .Include(x => x.Anciano).ThenInclude(x => x.Persona)
                                                .Include(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora).ToListAsync();
            return ofertas;
        }

        public async Task<Oferta> Save(Oferta entity)
        {
            var oferta = entity;
            context.Ofertas.Add(oferta);
            await context.SaveChangesAsync();
            return oferta;
        }

        public async Task<Oferta> Update(int id, Oferta entity)
        {
            var oferta = entity;
            var entityId = await context.Ofertas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                oferta.Id = entityId;
            }
            context.Entry(oferta).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return oferta;
        }
    }
}