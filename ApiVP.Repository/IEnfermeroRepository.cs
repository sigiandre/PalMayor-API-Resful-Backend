using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Domain.Entities;

namespace ApiVP.Repository
{
    public interface IEnfermeroRepository : IRepository<Enfermero>
    {
        Task<Enfermero> GetEnfermeroByCorreo(string correo);
    }
}