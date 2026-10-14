using Microsoft.EntityFrameworkCore;
using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Repositories;

public class BidRepository : GenericRepository<Bid>, IBidRepository
{
    public BidRepository(SubastaYaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Bid>> GetByAuctionIdAsync(int auctionId, CancellationToken cancellationToken)
    {
        return await _context.Set<Bid>()
            .Where(x => x.AuctionId == auctionId)
            .OrderByDescending(x => x.Amount)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Bid>> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Set<Bid>()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.BidDate)
            .ToListAsync(cancellationToken); ;
    }

    public async Task<Bid?> GetHighestBidByAuctionIdAsync(int auctionId, CancellationToken cancellationToken)
    {
        return await _context.Set<Bid>()
            .Where(x => x.AuctionId == auctionId)
            .OrderByDescending(x => x.Amount)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
