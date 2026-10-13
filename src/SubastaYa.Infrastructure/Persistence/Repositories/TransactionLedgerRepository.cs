using SubastaYa.Aplication.Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Repositories;

public class TransactionLedgerRepository : GenericRepository<TransactionLedger>, ITransactionLedgerRepository
{
    public TransactionLedgerRepository(SubastaYaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TransactionLedger>> GetAllByAuctionIdAsync(int auctionId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TransactionLedger>()
            .Where(tl => tl.AuctionId == auctionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TransactionLedger>> GetAllByWalletIdAsync(int walletId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TransactionLedger>()
            .Where(tl => tl.WalletId == walletId)
            .ToListAsync(cancellationToken);
    }
}
