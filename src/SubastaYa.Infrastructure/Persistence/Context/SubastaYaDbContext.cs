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
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
