namespace SubastaYa.Aplication.Dtos.AuditLog;

public class AuditLogResponseDto
{
    public int Id { get; set; }
    public string? Entity { get; set; }
    public int EntityId { get; set; }
    public string? Action { get; set; }

    public int? UserId { get; set; }
    public string? UserEmail { get; set; }

    public string JsonDetail { get; set; } = null!;
    public DateTime Date { get; set; }
}
