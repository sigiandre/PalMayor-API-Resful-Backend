using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Domain.Entities;

namespace ApiVP.Repository
{
    public interface IServicioRepository : IRepository<Servicio>
    {
        Task<List<Servicio>> GetByFamiliarCorreo(string correo);

        Task<List<Servicio>> GetByEnfermeroCorreo(string correo);
    }
}