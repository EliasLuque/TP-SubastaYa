namespace SubastaYa.Domain.Entities;

public enum TransactionType
{
    DEPOSIT,
    RETENTION,
    RELEASE,
    PAYMENT,
    COLLECTION
}

public class TransactionLedger : BaseEntity
{
    public int WalletId { get; set; }
    public int? AuctionId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    // Navigation Property
    public Wallet Wallet { get; set; } = null!;
    public Auction? Auction { get; set; }

}
