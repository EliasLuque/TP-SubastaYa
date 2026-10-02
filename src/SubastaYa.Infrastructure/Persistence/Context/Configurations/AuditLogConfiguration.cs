using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Infrastructure.Persistence.Context.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // Strings
        builder.Property(x => x.Entity).HasMaxLength(100);
        builder.Property(x => x.Action).HasMaxLength(50);

        // Json
        builder.Property(x => x.JsonDetail).HasColumnType("jsonb").IsRequired();

        builder.Property(x => x.Date).IsRequired();

        // Relación con User
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
