using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GeyByIdQuery;

public class GetBidByIdQuery : IRequest<BaseResponse<BidResponseDto>>
{
    public int Id { get; set; }
}
