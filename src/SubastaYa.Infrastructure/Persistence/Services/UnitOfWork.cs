using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly SubastaYaDbContext _context;

    public UnitOfWork(SubastaYaDbContext context)
    {
        _context = context;
    }

    public void Dispose() => _context.Dispose();

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
