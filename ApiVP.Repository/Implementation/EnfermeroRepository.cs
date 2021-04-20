using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class EnfermeroRepository : IEnfermeroRepository
    {
        private readonly ApplicationDbContext context;

        public EnfermeroRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var enfermeroId = await context.Enfermeros.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (enfermeroId == default(int))
            {
                return false;
            }
            context.Enfermeros.Remove(new Enfermero { Id = enfermeroId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Enfermero> Get(int id)
        {
            var enfermero = await context.Enfermeros.Include(x => x.Persona)
                                                    .Include(x => x.Especialidad)
                                                    .Include(x => x.Grado)
                                                    .Include(x => x.Servicios)
                                                    .Include(x => x.EnfermeroOfertas).ThenInclude(x => x.Oferta)
                                                    .FirstOrDefaultAsync(x => x.Id == id);
            return enfermero;
        }

        public async Task<List<Enfermero>> GetAll()
        {
            var enfermeros = await context.Enfermeros.Include(x => x.Especialidad)
                                                        .Include(x => x.Grado)
                                                        .Include(x => x.Persona)
                                                        .ToListAsync();
            return enfermeros;
        }

        public async Task<Enfermero> GetEnfermeroByCorreo(string correo)
        {
            var enfermero = await context.Enfermeros.Where(x => x.Usuario.Correo == correo)
                                                    .Include(x => x.Persona)
                                                    .Include(x => x.Especialidad)
                                                    .Include(x => x.Grado)
                                                    .FirstOrDefaultAsync();

            return enfermero;
        }

        public async Task<Enfermero> Save(Enfermero entity)
        {
            var enfermero = entity;
            context.Enfermeros.Add(enfermero);
            await context.SaveChangesAsync();
            return enfermero;
        }

        public async Task<Enfermero> Update(int id, Enfermero entity)
        {
            var enfermero = entity;
            var entityId = await context.Enfermeros.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                enfermero.Id = entityId;
            }
            context.Entry(enfermero).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return enfermero;
        }
    }
}