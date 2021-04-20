using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Domain.Entities;

namespace ApiVP.Repository
{
    public interface IAncianoRepository : IRepository<Anciano>
    {
        Task<List<Anciano>> GetByCorreo(string param);
    }
}