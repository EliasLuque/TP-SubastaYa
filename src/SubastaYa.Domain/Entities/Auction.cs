using System.Reflection.Metadata.Ecma335;

namespace SubastaYa.Domain.Entities;

public enum AuctionStatus
{
    ACTIVE = 1,
    SCHEDULED = 2,
    FINISHED = 3,
    UNSOLD = 4
}

public class Auction : BaseEntity
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
    public Category Category { get; set; }
}
