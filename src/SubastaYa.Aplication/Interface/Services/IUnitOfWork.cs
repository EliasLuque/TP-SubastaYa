using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Auction> Auction { get; }
    IGenericRepository<AuditLog> AuditLog{ get; }
    IGenericRepository<Bid> Bid { get; }
    IGenericRepository<Category> Category{ get; }
    IGenericRepository<TransactionLedger> TransactionLedger{ get; }
    IGenericRepository<User> User { get; }
    IGenericRepository<Wallet> Wallet{ get; }
    Task SaveChangesAsync();
}
