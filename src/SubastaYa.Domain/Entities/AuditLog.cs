using System.ComponentModel.DataAnnotations;

namespace SubastaYa.Domain.Entities;

public class AuditLog : BaseEntity
{
    public string? Entity { get; set; }
    public int EntityId { get; set; }
    public string? Action { get; set; }
    public int? UserId { get; set; }
    public string JsonDetail { get; set; } = null!;
    public DateTime Date { get; set; }

    // Navigation Property
    public User? User { get; set; }
}