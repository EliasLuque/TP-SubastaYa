using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetByIdQuery;

public class GetAuctionByIdQuery : IRequest<BaseResponse<AuctionResponseDto>>
{
    public int Id { get; set; }
}
