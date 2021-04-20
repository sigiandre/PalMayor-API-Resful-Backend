using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class ABVCRepository : IABVCRepository
    {
        private readonly ApplicationDbContext context;

        public ABVCRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var abvcId = await context.ABVCs.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (abvcId == default(int))
            {
                return false;
            }
            context.ABVCs.Remove(new ABVC { Id = abvcId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ABVC> Get(int id)
        {
            var abvc = await context.ABVCs.Include(x => x.Tipo).FirstOrDefaultAsync(x => x.Id == id);
            return abvc;
        }

        public async Task<List<ABVC>> GetAll()
        {
            var abvcs = await context.ABVCs.Include(x => x.Tipo).ToListAsync();
            return abvcs;
        }

        public async Task<ABVC> Save(ABVC entity)
        {
            var abvc = entity;
            context.ABVCs.Add(abvc);
            await context.SaveChangesAsync();
            return abvc;
        }

        public async Task<ABVC> Update(int id, ABVC entity)
        {
            var abvc = entity;
            var entityId = await context.ABVCs.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                abvc.Id = entityId;
            }
            context.Entry(abvc).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return abvc;
        }
    }
}