using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class GradoRepository : IGradoRepository
    {
        private readonly ApplicationDbContext context;

        public GradoRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Delete(int id)
        {
            var gradoId = await context.Grados.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (gradoId == default(int))
            {
                return false;
            }
            context.Grados.Remove(new Grado { Id = gradoId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Grado> Get(int id)
        {
            var grado = await context.Grados.FirstOrDefaultAsync(x => x.Id == id);
            return grado;
        }

        public async Task<List<Grado>> GetAll()
        {
            var grados = await context.Grados.ToListAsync();
            return grados;
        }

        public async Task<Grado> Save(Grado entity)
        {
            var grado = entity;
            context.Grados.Add(grado);
            await context.SaveChangesAsync();
            return grado;
        }

        public async Task<Grado> Update(int id, Grado entity)
        {
            var grado = entity;
            var entityId = await context.Grados.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                grado.Id = entityId;
            }
            context.Entry(grado).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return grado;
        }
    }
}