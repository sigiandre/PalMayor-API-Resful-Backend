using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Domain.Entities;

namespace ApiVP.Repository
{
    public interface IEnfermeroOfertaRepository : IRepository<EnfermeroOferta>
    {
        Task<List<EnfermeroOferta>> GetAllByEnfermeroCorreo(string correo);
        Task<EnfermeroOferta> GetByEnfermeroByOferta(int enfermeroid, int ofertaid);
        Task<List<EnfermeroOferta>> GetAllByOferta(int id);
    }
}