using System.Collections.Generic;
using ApiVP.Domain.Entities;
using System.Threading.Tasks;

namespace ApiVP.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<List<string>> GetAllCorreos();
        Task<Usuario> Login(Usuario entity);
    }
}