using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class FechaAtencionRepository : IFechaAtencionRepository
    {
        private readonly ApplicationDbContext context;

        public FechaAtencionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var fechaAtencionId = await context.FechaAtenciones.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (fechaAtencionId == default(int))
            {
                return false;
            }
            context.FechaAtenciones.Remove(new FechaAtencion { Id = fechaAtencionId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<FechaAtencion> Get(int id)
        {
            var fechaAtencion = await context.FechaAtenciones.Include(x => x.RangoHora).FirstOrDefaultAsync(x => x.Id == id);
            return fechaAtencion;
        }

        public async Task<List<FechaAtencion>> GetAll()
        {
            var fechaAtenciones = await context.FechaAtenciones.Include(x => x.RangoHora).ToListAsync();
            return fechaAtenciones;
        }

        public async Task<FechaAtencion> Save(FechaAtencion entity)
        {
            var fechaAtencion = entity;
            context.FechaAtenciones.Add(fechaAtencion);
            await context.SaveChangesAsync();
            return fechaAtencion;
        }

        public async Task<FechaAtencion> Update(int id, FechaAtencion entity)
        {
            var fechaAtencion = entity;
            var entityId = await context.FechaAtenciones.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                fechaAtencion.Id = entityId;
            }
            context.Entry(fechaAtencion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return fechaAtencion;
        }
    }
}