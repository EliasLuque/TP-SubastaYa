using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Bids.Commands.CreateCommand;

public class CreateBidCommand : IRequest<BaseResponse<int>>
{
    public int AuctionId { get; set; }
    public decimal Amount { get; set; }

    [JsonIgnore]
    public int UserId { get; set; }
}
