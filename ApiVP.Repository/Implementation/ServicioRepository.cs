using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly ApplicationDbContext context;

        public ServicioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var servicioId = await context.Servicios.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (servicioId == default(int))
            {
                return false;
            }
            context.Servicios.Remove(new Servicio { Id = servicioId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Servicio> Get(int id)
        {
            var servicio = await context.Servicios.Include(x => x.Enfermero).ThenInclude(x => x.Persona)
                                                    .Include(x => x.Oferta).ThenInclude(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora)
                                                    .FirstOrDefaultAsync(x => x.Id == id);

            var anciano = await context.Ancianos.Include(x => x.Persona).FirstOrDefaultAsync(x => x.Id == servicio.Oferta.AncianoId);

            var especialidad = await context.Especialidades.FirstOrDefaultAsync(x => x.Id == servicio.Enfermero.EspecialidadId);

            servicio.Enfermero.Especialidad = especialidad;
            servicio.Oferta.Anciano = anciano;

            return servicio;
        }

        public async Task<List<Servicio>> GetAll()
        {
            var servicios = await context.Servicios.Include(x => x.Oferta).Include(x => x.Enfermero).ToListAsync();
            return servicios;
        }

        public async Task<List<Servicio>> GetByEnfermeroCorreo(string correo)
        {
            var enfermero = await context.Enfermeros.Where(x => x.Usuario.Correo == correo).FirstOrDefaultAsync();
            if (enfermero == null)
            {
                return null;
            }

            var servicios = await context.Servicios.Where(x => x.Enfermero.Id == enfermero.Id)
                                                    .Include(x => x.Oferta).ThenInclude(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora)
                                                    .Include(x => x.Enfermero).ThenInclude(x => x.Persona).ToListAsync();

            var ancianos = await context.Ancianos.Include(x => x.Persona).ToListAsync();

            return servicios;
        }

        public async Task<List<Servicio>> GetByFamiliarCorreo(string correo)
        {
            var familiar = await context.Familiares.Where(x => x.Usuario.Correo == correo).FirstOrDefaultAsync();
            if (familiar == null)
            {
                return null;
            }

            var servicios = await context.Servicios.Where(x => x.Oferta.Anciano.FamiliarId == familiar.Id)
                                                    .Include(x => x.Oferta).ThenInclude(x => x.FechaAtenciones).ThenInclude(x => x.RangoHora)
                                                    .Include(x => x.Enfermero).ThenInclude(x => x.Persona).ToListAsync();

            var ancianos = await context.Ancianos.Where(x => x.FamiliarId == familiar.Id).Include(x => x.Persona).ToListAsync();

            foreach (var s in servicios)
            {
                foreach (var a in ancianos)
                {
                    if (s.Oferta.AncianoId == a.Id)
                    {
                        s.Oferta.Anciano = a;
                    }
                }
            }

            return servicios;
        }

        public async Task<Servicio> Save(Servicio entity)
        {
            var servicio = entity;
            context.Servicios.Add(servicio);
            await context.SaveChangesAsync();
            return servicio;
        }

        public async Task<Servicio> Update(int id, Servicio entity)
        {
            var servicio = entity;
            var entityId = await context.Servicios.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                servicio.Id = entityId;
            }
            context.Entry(servicio).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return servicio;
        }
    }
}
