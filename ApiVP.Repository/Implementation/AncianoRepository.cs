using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class AncianoRepository : IAncianoRepository
    {
        private readonly ApplicationDbContext context;

        public AncianoRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var ancianoId = await context.Ancianos.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (ancianoId == default(int))
            {
                return false;
            }
            context.Ancianos.Remove(new Anciano { Id = ancianoId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Anciano> Get(int id)
        {
            var anciano = await context.Ancianos.Include(x => x.Persona)
                                                .Include(x => x.AncianoABVCs).ThenInclude(x => x.ABVC)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return anciano;
        }

        public async Task<List<Anciano>> GetAll()
        {
            var ancianos = await context.Ancianos.Include(x => x.Persona).ToListAsync();
            return ancianos;
        }

        public async Task<List<Anciano>> GetByCorreo(string param)
        {
            var ancianos = await context.Ancianos.Where(x => x.Familiar.Usuario.Correo == param).Include(x => x.Persona)
                                                                                            .Include(x => x.AncianoABVCs).ThenInclude(x => x.ABVC).ThenInclude(x => x.Tipo)
                                                                                            .ToListAsync();
            return ancianos;
        }

        public async Task<Anciano> Save(Anciano entity)
        {
            var anciano = entity;
            context.Ancianos.Add(anciano);
            await context.SaveChangesAsync();
            return anciano;
        }

        public async Task<Anciano> Update(int id, Anciano entity)
        {
            var anciano = entity;
            var entityId = await context.Ancianos.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                anciano.Id = entityId;
            }
            context.Entry(anciano).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return anciano;
        }
    }
}