namespace SubastaYa.Domain.Entities;

public class Wallet : BaseEntity
{
    public int UserId { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal HeldBalance { get; set; }
    public decimal AvailableBalance { get; set; }

    // Optimistic Locking
    public int Version { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;

    // Relación 1 a muchos
    public ICollection<TransactionLedger> TrasactionLedger { get; set; } = null!;
}
