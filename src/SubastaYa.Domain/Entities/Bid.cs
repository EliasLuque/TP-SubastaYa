using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata;

namespace SubastaYa.Domain.Entities;

public class Bid : BaseEntity
{
    public int AuctionId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime BidDate { get; set; }

    // Navigation Property
    public Auction Auction { get; set; } = null!;
    public User User { get; set; } = null!;
}
