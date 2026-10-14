using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetBySellerIdQuery;

public class GetAuctionsBySellerIdQuery : IRequest<BaseResponse<IEnumerable<AuctionResponseDto>>>
{
    [JsonIgnore]
    public int SellerId { get; set; }
}
