using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Domain.Entities;

namespace ApiVP.Repository
{
    public interface IOfertaRepository : IRepository<Oferta>
    {
        Task<List<Oferta>> GetOfertasByCorreo(string param);
    }
}