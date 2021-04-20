using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class RangoHoraRepository : IRangoHoraRepository
    {
        private readonly ApplicationDbContext context;

        public RangoHoraRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var rangohoraId = await context.RangoHoras.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (rangohoraId == default(int))
            {
                return false;
            }
            context.RangoHoras.Remove(new RangoHora { Id = rangohoraId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<RangoHora> Get(int id)
        {
            var rangohora = await context.RangoHoras.FirstOrDefaultAsync(x => x.Id == id);
            return rangohora;
        }

        public async Task<List<RangoHora>> GetAll()
        {
            var rangohoras = await context.RangoHoras.ToListAsync();
            return rangohoras;
        }

        public async Task<RangoHora> Save(RangoHora entity)
        {
            var rangohora = entity;
            context.RangoHoras.Add(rangohora);
            await context.SaveChangesAsync();
            return rangohora;
        }

        public async Task<RangoHora> Update(int id, RangoHora entity)
        {
            var rangohora = entity;
            var entityId = await context.RangoHoras.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                rangohora.Id = entityId;
            }
            context.Entry(rangohora).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return rangohora;
        }
    }
}