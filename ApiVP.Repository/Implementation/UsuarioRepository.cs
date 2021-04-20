using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;
using ApiVP.Repository.Context;

namespace ApiVP.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var usuarioId = await context.Usuarios.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (usuarioId == default(int))
            {
                return false;
            }
            context.Usuarios.Remove(new Usuario { Id = usuarioId });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> Get(int id)
        {
            var usuario = await context.Usuarios.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Id == id);
            return usuario;
        }

        public async Task<List<Usuario>> GetAll()
        {
            var usuarios = await context.Usuarios.Include(x => x.Rol).ToListAsync();
            return usuarios;
        }

        public async Task<List<string>> GetAllCorreos()
        {
            var usuarios = await context.Usuarios.Select(x => x.Correo).ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> Login(Usuario entity)
        {
            var usuarioCorreo = entity.Correo;
            var usuarioContra = entity.Contrasenya;
            var usuario = await context.Usuarios.Where(x => x.Correo == usuarioCorreo).Where(x => x.Contrasenya == usuarioContra)
                                .Include(x => x.Rol).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }

        public async Task<Usuario> Save(Usuario entity)
        {
            var usuario = entity;
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Update(int id, Usuario entity)
        {
            var usuario = entity;
            var entityId = await context.Usuarios.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (entityId == default(int))
            {
                return null;
            }
            else
            {
                usuario.Id = entityId;
            }
            context.Entry(usuario).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return usuario;
        }
    }
}