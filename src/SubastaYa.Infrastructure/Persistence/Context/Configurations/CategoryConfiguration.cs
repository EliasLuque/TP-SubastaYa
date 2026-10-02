using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Infrastructure.Persistence.Context.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // String
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IconUrl).IsRequired(false).HasMaxLength(2048);

        // Relación con Auction
        builder.HasMany(x => x.Auctions)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
