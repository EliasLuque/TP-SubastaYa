using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GetAllQuery;

public class GetAllBidQuery : IRequest<BaseResponse<IEnumerable<BidResponseDto>>>
{
}
