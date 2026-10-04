using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly SubastaYaDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(SubastaYaDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }
    public IQueryable<T> GetQueryable()
    {
        var response = _context.Set<T>().AsNoTracking();
        return response;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var response = await _entity.SingleOrDefaultAsync(x => x.Id == id)!;
        return response!;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
    }


    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(IConfigurationProvider mapperConfig)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ProjectTo<TDto>(mapperConfig)
            .ToListAsync();
    }

    public async Task<TDto?> GetByIdProjectedAsync<TDto>(int id, IConfigurationProvider mapperConfig)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .ProjectTo<TDto>(mapperConfig)
            .FirstOrDefaultAsync();
    }
}
