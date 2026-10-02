using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Dtos.Auction;

public class AuctionResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal MinimunIncrement { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AuctionStatus Status { get; set; }
    public string StatusDescription { get; set; } = null!;
    public int SellerId { get; set; }

    public string CategoryName { get; set; } = null!;
    public int TotalBids { get; set; }
}
