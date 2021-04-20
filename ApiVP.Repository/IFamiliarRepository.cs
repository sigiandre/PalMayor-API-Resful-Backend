using ApiVP.Domain.Entities;
using System.Threading.Tasks;

namespace ApiVP.Repository
{
    public interface IFamiliarRepository : IRepository<Familiar>
    {
        Task<Familiar> GetByCorreo(string param);
    }
}