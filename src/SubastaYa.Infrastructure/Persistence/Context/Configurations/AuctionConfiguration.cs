using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Infrastructure.Persistence.Context.Configurations;

public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        // Key
        builder.HasKey(x => x.Id);
        
        // Strings
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.ImageUrl).HasMaxLength(2048);
        
        // Decimals
        builder.Property(x => x.BasePrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CurrentPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.MinimumIncrement).HasColumnType("decimal(18,2)");
        
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        // Optimistic Locking
        builder.Property(x => x.Version).IsConcurrencyToken().IsRequired();

        // Relación con Category
        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación con Bids
        builder.HasMany(x => x.Bids)
            .WithOne()
            .HasForeignKey("AuctionId")
            .OnDelete(DeleteBehavior.Cascade);

        // Relación con TransactionLedger
        builder.HasMany(x => x.TransactionLedger)
            .WithOne()
            .HasForeignKey("AuctionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(x => x.State == Domain.Entities.EntityState.ACTIVE);
    }
}
