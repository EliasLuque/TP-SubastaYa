using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.UpdateCommand;

public class UpdateAuctionCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }

    [JsonIgnore]
    public int SellerId { get; set; }

    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public decimal MinimumIncrement { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
