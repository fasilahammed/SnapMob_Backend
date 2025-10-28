using SnapMob_Backend.Models;

namespace SnapMob_Backend.Repositories.interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        IQueryable<T> GetQueryable();


    }
}
