namespace SubastaYa.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? IconUrl { get; set; }

    // Coleccion para la relación 1 a muchos
    public ICollection<Auction> Auctions { get; set; } = null!;
}
