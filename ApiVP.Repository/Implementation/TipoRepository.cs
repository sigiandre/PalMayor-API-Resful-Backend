using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class TipoRepository : ITipoRepository
    {
        private readonly ApplicationDbContext context;

        public TipoRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var tipoId = await context.Tipos.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (tipoId == default(int))
            {
                return false;
            }
            context.Tipos.Remove(new Tipo { Id = tipoId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Tipo> Get(int id)
        {
            var tipo = await context.Tipos.FirstOrDefaultAsync(x => x.Id == id);
            return tipo;
        }

        public async Task<List<Tipo>> GetAll()
        {
            var tipos = await context.Tipos.ToListAsync();
            return tipos;
        }

        public async Task<Tipo> Save(Tipo entity)
        {
            var tipo = entity;
            context.Tipos.Add(tipo);
            await context.SaveChangesAsync();
            return tipo;
        }

        public async Task<Tipo> Update(int id, Tipo entity)
        {
            var tipo = entity;
            var entityId = await context.Tipos.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                tipo.Id = entityId;
            }
            context.Entry(tipo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return tipo;
        }
    }
}