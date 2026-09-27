using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetAllQuery;

public class GetAllAuctionQuery : IRequest<BaseResponse<IEnumerable<AuctionResponseDto>>>
{
}
