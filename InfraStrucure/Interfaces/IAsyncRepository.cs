using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfraStrucure.Interfaces
{
    public interface IAsyncRepository<T, Key>
    {
        Task<T> GetByIdAsync(Key id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(dynamic parameters);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }

    public interface IAsyncRepository<T, Key1, Key2>
    {
        Task<T> GetByIdAsync(Key1 id1, Key2 id2);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(dynamic parameters);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
