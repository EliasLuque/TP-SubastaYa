namespace SubastaYa.Aplication.Dtos.Bid;

public class BidResponseDto
{
    public int Id { get; set; }
    
    public int AuctionId { get; set; }
    public string AuctionTitle { get; set; } = null!;

    public decimal Amount { get; set; }
    public DateTime BidDate { get; set; }

    public string UserEmail { get; set; } = null!;
}
