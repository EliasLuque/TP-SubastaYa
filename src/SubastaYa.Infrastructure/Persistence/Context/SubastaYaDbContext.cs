using Microsoft.EntityFrameworkCore;
using SubastaYa.Domain.Entities;
using System.Reflection;

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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
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
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
