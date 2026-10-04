using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Infrastructure.Persistence.Context.Configurations;

internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // Decimal
        builder.Property(x => x.TotalBalance).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.AvailableBalance).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.HeldBalance).HasColumnType("decimal(18,2)").IsRequired();

        // Optimistic Locking
        builder.Property(x => x.Version).IsConcurrencyToken().IsRequired();

        // Relación con User
        builder.HasOne(x => x.User)
            .WithOne(x => x.Wallet)
            .HasForeignKey<Wallet>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación con TransactionLedger
        builder.HasMany(x => x.TrasactionLedger)
            .WithOne(x => x.Wallet)
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => x.State == Domain.Entities.EntityState.ACTIVE);
    }
}
