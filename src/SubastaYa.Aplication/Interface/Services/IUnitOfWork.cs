using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Services;

public interface IUnitOfWork : IDisposable
{
    IAuctionRepository Auction { get; }
    IGenericRepository<AuditLog> AuditLog{ get; }
    IBidRepository Bid { get; }
    IGenericRepository<Category> Category{ get; }
    ITransactionLedgerRepository TransactionLedger { get; }
    IUserRepository User { get; }
    IWalletRepository Wallet { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
