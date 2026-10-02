using System.Security.Principal;

namespace SubastaYa.Aplication.Dtos.Auction;

public class AuctionCreateDto
{
    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public decimal BasePrice { get; set; }
    public decimal MinimunIncrement { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }   
}
