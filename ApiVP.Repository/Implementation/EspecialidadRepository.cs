using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly ApplicationDbContext context;

        public EspecialidadRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var especialidadId = await context.Especialidades.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (especialidadId == default(int))
            {
                return false;
            }
            context.Especialidades.Remove(new Especialidad { Id = especialidadId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Especialidad> Get(int id)
        {
            var especialidad = await context.Especialidades.FirstOrDefaultAsync(x => x.Id == id);
            return especialidad;
        }

        public async Task<List<Especialidad>> GetAll()
        {
            var especialidades = await context.Especialidades.ToListAsync();
            return especialidades;
        }

        public async Task<Especialidad> Save(Especialidad entity)
        {
            var especialidad = entity;
            context.Especialidades.Add(especialidad);
            await context.SaveChangesAsync();
            return especialidad;
        }

        public async Task<Especialidad> Update(int id, Especialidad entity)
        {
            var especialidad = entity;
            var entityId = await context.Especialidades.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                especialidad.Id = entityId;
            }
            context.Entry(especialidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return especialidad;
        }
    }
}