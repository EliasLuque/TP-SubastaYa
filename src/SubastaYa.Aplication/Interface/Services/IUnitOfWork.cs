using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Auction> Auction { get; }
    IGenericRepository<Category> Category { get; }
    Task SaveChangesAsync();
}
