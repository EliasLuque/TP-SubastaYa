using Microsoft.EntityFrameworkCore;

namespace SubastaYa.Infrastructure.Persistence.Context;

public class SubastaYaDbContext : DbContext
{
    public SubastaYaDbContext(DbContextOptions<SubastaYaDbContext> contextOptions) : base(contextOptions) 
    {
    }
}
