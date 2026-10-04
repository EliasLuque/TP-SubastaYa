using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;

public class DeleteAuctionCommand : IRequest<BaseResponse<int>>
{
    [JsonIgnore]
    public int SellerId { get; set; }
    public int Id { get; set; }
}
