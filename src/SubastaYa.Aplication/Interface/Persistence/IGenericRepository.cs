using AutoMapper;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetQueryable();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(IConfigurationProvider mapperConfig);
    Task<TDto?> GetByIdProjectedAsync<TDto>(int id, IConfigurationProvider mapperConfig);
}
