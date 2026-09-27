using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.CreateCommand;

public class CreateAuctionCommand : IRequest<BaseResponse<bool>>
{
    public int SellerId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal MinimumIncrement { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
    public int Version { get; set; }
}
