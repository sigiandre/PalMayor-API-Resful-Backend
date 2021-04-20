using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class EnfermeroOfertaRepository : IEnfermeroOfertaRepository
    {
        private readonly ApplicationDbContext context;

        public EnfermeroOfertaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<EnfermeroOferta> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<EnfermeroOferta>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<EnfermeroOferta> Save(EnfermeroOferta entity)
        {
            var enfermeroOferta = entity;
            context.EnfermeroOfertas.Add(enfermeroOferta);
            await context.SaveChangesAsync();
            return enfermeroOferta;
        }

        public Task<EnfermeroOferta> Update(int id, EnfermeroOferta entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<EnfermeroOferta> GetByEnfermeroByOferta(int enfermeroid, int ofertaid)
        {
            var enfermeroOferta = await context.EnfermeroOfertas.Where(x => x.EnfermeroId == enfermeroid).Where(x => x.OfertaId == ofertaid).FirstOrDefaultAsync();
            return enfermeroOferta;
        }

        public async Task<List<EnfermeroOferta>> GetAllByEnfermeroCorreo(string correo)
        {
            var enfermeroOfertas = await context.EnfermeroOfertas.Where(x => x.Enfermero.Usuario.Correo == correo)
                                                                .Include(x => x.Oferta).ThenInclude(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora)
                                                                .ToListAsync();

            var ancianos = await context.Ancianos.Include(x => x.Persona).ToListAsync();
            //var familiares = await context.Familiares.Include(x => x.Persona).ToListAsync();
            return enfermeroOfertas;
        }

        public async Task<List<EnfermeroOferta>> GetAllByOferta(int id)
        {
            var enfermeroOfertas = await context.EnfermeroOfertas.Where(x => x.OfertaId == id)
                                                                .Include(x => x.Enfermero).ThenInclude(x => x.Persona)
                                                                .ToListAsync();

            var especialidades = await context.Especialidades.ToListAsync();
            var grados = await context.Grados.ToListAsync();
            var servicios = await context.Servicios.ToListAsync();

            return enfermeroOfertas;
        }

    }
}