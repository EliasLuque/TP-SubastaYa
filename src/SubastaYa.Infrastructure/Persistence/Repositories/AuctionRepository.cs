using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Repositories;

public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
{
    public AuctionRepository(SubastaYaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Auction>> GetBySellerIdAsync(int sellerId, CancellationToken cancellationToken)
    {
        return await _context.Set<Auction>()
            .Where(x => x.SellerId == sellerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AuctionResponseDto>> GetCatalogAsync(int? categoryId, AuctionStatus? status, IMapper mapper, CancellationToken cancellationToken)
    {
        var query = _context.Set<Auction>().AsQueryable();

        if (status != null)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        if (categoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == categoryId.Value);
        }

        return await query
            .ProjectTo<AuctionResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Auction>> GetExpiredActiveAuctionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Auction>()
            .Where(x => x.Status == AuctionStatus.ACTIVE && x.EndDate <= DateTime.UtcNow)
            .ToListAsync(cancellationToken);
    }
}
