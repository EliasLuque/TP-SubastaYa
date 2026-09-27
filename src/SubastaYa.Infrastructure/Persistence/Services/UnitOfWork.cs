using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;
using SubastaYa.Infrastructure.Persistence.Repositories;

namespace SubastaYa.Infrastructure.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly SubastaYaDbContext _context;
    private readonly IGenericRepository<Auction> _auction = null!;
    private readonly IGenericRepository<Category> _category = null!;

    public UnitOfWork(SubastaYaDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Auction> Auction => _auction ?? new GenericRepository<Auction>(_context);
    public IGenericRepository<Category> Category => _category ?? new GenericRepository<Category>(_context);

    public void Dispose() => _context.Dispose();
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
