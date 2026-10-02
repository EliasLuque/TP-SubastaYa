using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Dtos.TransactionLedger;

public class TransactionLedgerResponseDto
{
    public int Id { get; set; }

    public TransactionType Type { get; set; }
    public string TypeDescription { get; set; } = null!;
    
    public int? AuctionId { get; set; }
    public string? AuctionTitle { get; set; }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
