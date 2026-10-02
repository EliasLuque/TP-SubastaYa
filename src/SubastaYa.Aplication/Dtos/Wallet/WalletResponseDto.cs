namespace SubastaYa.Aplication.Dtos.Wallet;

public class WalletResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public decimal TotalBalance { get; set; }
    public decimal HeldBalance { get; set; }
    public decimal AvailableBalance { get; set; }
}
