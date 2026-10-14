using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetAllQuery;

public class GetAllAuctionQuery : IRequest<BaseResponse<IEnumerable<AuctionResponseDto>>>
{
    public int? CategoryId { get; set; }
    public AuctionStatus? Status { get; set; } = AuctionStatus.ACTIVE;
}
