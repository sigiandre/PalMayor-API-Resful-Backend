using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext context;

        public RolRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var rolId = await context.Roles.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (rolId == default(int))
            {
                return false;
            }
            context.Roles.Remove(new Rol { Id = rolId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Rol> Get(int id)
        {
            var rol = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return rol;
        }

        public async Task<List<Rol>> GetAll()
        {
            var roles = await context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Rol> Save(Rol entity)
        {
            var rol = entity;
            context.Roles.Add(rol);
            await context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol> Update(int id, Rol entity)
        {
            var rol = entity;
            var entityId = await context.Roles.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                rol.Id = entityId;
            }
            context.Entry(rol).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return rol;
        }
    }
}