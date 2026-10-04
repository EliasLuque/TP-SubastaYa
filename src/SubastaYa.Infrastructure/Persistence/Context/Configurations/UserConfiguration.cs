using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Infrastructure.Persistence.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // Strings
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(500);
        
        builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.RegistrationDate).IsRequired();

        // Relación con Wallet
        builder.HasOne(x => x.Wallet)
            .WithOne()
            .HasForeignKey<Wallet>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación con Auction
        builder.HasMany(x => x.Auctions)
            .WithOne()
            .HasForeignKey("SellerId")
            .OnDelete(DeleteBehavior.Restrict);

        // Relación con AuditLog
        builder.HasMany(x => x.AuditLogs)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relación con Bid
        builder.HasMany(x => x.Bids)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => x.State == Domain.Entities.EntityState.ACTIVE);
    }
}
