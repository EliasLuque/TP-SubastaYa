using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Infrastructure.Persistence.Context.Configurations;

public class TransactionLedgerConfiguration : IEntityTypeConfiguration<TransactionLedger>
{
    public void Configure(EntityTypeBuilder<TransactionLedger> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // Decimal
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();

        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Type).IsRequired();

        // Relación con Wallet
        builder.HasOne(x => x.Wallet)
            .WithMany(x => x.TrasactionLedger)
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación con Auction
        builder.HasOne(x => x.Auction)
            .WithMany(x => x.TransactionLedger)
            .HasForeignKey(x => x.AuctionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(x => x.State == Domain.Entities.EntityState.ACTIVE);
    }
}
