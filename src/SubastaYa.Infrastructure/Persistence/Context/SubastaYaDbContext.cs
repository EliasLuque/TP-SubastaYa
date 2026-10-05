using Microsoft.EntityFrameworkCore;
using SubastaYa.Domain.Entities;
using System.Reflection;
using System.Text.Json;

namespace SubastaYa.Infrastructure.Persistence.Context;

public class SubastaYaDbContext : DbContext
{
    public SubastaYaDbContext(DbContextOptions<SubastaYaDbContext> contextOptions) : base(contextOptions) 
    {
    }

    public DbSet<Auction> Auctions { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TransactionLedger> TransactionLedger { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.Entity is AuditLog)
                continue;

            switch (entry.State)
            {
                case Microsoft.EntityFrameworkCore.EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.State = Domain.Entities.EntityState.ACTIVE; 
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    if (entry.Entity.State == Domain.Entities.EntityState.ACTIVE)
                    {
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                    }
                    break;
            }

            if(entry.State == Microsoft.EntityFrameworkCore.EntityState.Added || 
                entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified ||
                entry.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
            {
                var jsonValue = entry.State == Microsoft.EntityFrameworkCore.EntityState.Deleted ?
                    JsonSerializer.Serialize(entry.OriginalValues.ToObject()) :
                    JsonSerializer.Serialize(entry.CurrentValues.ToObject());

                var audit = new AuditLog()
                {
                    Entity = entry.Entity.GetType().Name,
                    EntityId = entry.Entity.Id,
                    Action = entry.State.ToString(),
                    Date = DateTime.UtcNow,
                    JsonDetail = jsonValue
                };

                auditEntries.Add(audit);
            }
        }

        if(auditEntries.Any())
        {
            await Set<AuditLog>().AddRangeAsync(auditEntries, cancellationToken);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
