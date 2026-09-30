namespace SubastaYa.Aplication.Dtos.Auction;

public class AuctionResponseDto
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal MinimumIncrement { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
    public string StatusDescription { get; set; } = null!;
    public int BidCount { get; set; }
    public int Version { get; set; }
}
