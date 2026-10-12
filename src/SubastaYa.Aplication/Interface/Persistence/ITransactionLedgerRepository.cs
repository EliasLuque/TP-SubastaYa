using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface ITransactionLedgerRepository : IGenericRepository<TransactionLedger>
{
    Task<IEnumerable<TransactionLedger>> GetAllByWalletIdAsync(int walletId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TransactionLedger>> GetAllByAuctionIdAsync(int auctionId, CancellationToken cancellationToken = default);
}
