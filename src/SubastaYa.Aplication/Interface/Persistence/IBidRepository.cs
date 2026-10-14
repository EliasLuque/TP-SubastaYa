using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface IBidRepository : IGenericRepository<Bid>
{
    Task<Bid?> GetHighestBidByAuctionIdAsync(int auctionId, CancellationToken cancellationToken);
    Task<IEnumerable<Bid>> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<IEnumerable<Bid>> GetByAuctionIdAsync(int auctionId, CancellationToken cancellationToken);
}
