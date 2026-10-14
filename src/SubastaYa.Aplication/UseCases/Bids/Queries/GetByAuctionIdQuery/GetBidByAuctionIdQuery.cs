using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GetByAuctionIdQuery;

public class GetBidByAuctionIdQuery : IRequest<BaseResponse<IEnumerable<BidResponseDto>>>
{
    public int AuctionId { get; set; }
}
