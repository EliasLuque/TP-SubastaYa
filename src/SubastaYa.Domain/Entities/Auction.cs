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
    public AuctionStatus Status { get; set; }

    // Optimistic Locking
    public int Version { get; set; }

    // Navigation Propierty
    public Category Category { get; set; } = null!;

    // Colecciones para la relación 1 a muchos
    public ICollection<Bid> Bids { get; set; } = null!;
    public ICollection<TransactionLedger> TransactionLedger { get; set; } = null!;
}
