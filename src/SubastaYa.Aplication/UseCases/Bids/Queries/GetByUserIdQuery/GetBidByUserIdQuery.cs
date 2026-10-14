using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GetByUserIdQuery;

public class GetBidByUserIdQuery : IRequest<BaseResponse<IEnumerable<BidResponseDto>>>
{
    [JsonIgnore]
    public int UserId { get; set; }
}
