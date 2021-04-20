using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class FamiliarRepository : IFamiliarRepository
    {
        private readonly ApplicationDbContext context;

        public FamiliarRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var familiarId = await context.Familiares.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (familiarId == default(int))
            {
                return false;
            }
            context.Familiares.Remove(new Familiar { Id = familiarId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Familiar> Get(int id)
        {
            var familiar = await context.Familiares.Include(x => x.Ancianos).Include(x => x.Persona).Include(x => x.Usuario).FirstOrDefaultAsync(x => x.Id == id);
            return familiar;
        }

        public async Task<List<Familiar>> GetAll()
        {
            var familiares = await context.Familiares.Include(x => x.Persona).Include(x => x.Usuario).ToListAsync();
            return familiares;
        }

        public async Task<Familiar> GetByCorreo(string param)
        {
            //var id = await context.Usuarios.Where(x => x.Correo == param).Select(x => x.Id).FirstOrDefaultAsync();
            var familiar = await context.Familiares.Where(x => x.Usuario.Correo == param).Include(x => x.Ancianos).Include(x => x.Persona).FirstOrDefaultAsync();
            if (familiar == null)
            {
                return null;
            }
            return familiar;
        }

        public async Task<Familiar> Save(Familiar entity)
        {
            var familiar = entity;
            context.Familiares.Add(familiar);
            await context.SaveChangesAsync();
            return familiar;
        }

        public async Task<Familiar> Update(int id, Familiar entity)
        {
            var familiar = entity;
            var entityId = await context.Familiares.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                familiar.Id = entityId;
            }
            context.Entry(familiar).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return familiar;
        }
    }

}