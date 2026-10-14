using AutoMapper;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface IAuctionRepository : IGenericRepository<Auction>
{
    Task<IEnumerable<AuctionResponseDto>> GetCatalogAsync(int? categoryId, AuctionStatus? status, IMapper mapper, CancellationToken cancellationToken);
    Task<IEnumerable<Auction>> GetBySellerIdAsync(int sellerId, CancellationToken cancellationToken);
    Task<IEnumerable<Auction>> GetExpiredActiveAuctionAsync(CancellationToken cancellationToken);
}
