using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiVP.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Save(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}