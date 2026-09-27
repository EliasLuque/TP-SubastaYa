using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
