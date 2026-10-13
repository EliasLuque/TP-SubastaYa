using Microsoft.EntityFrameworkCore;
using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SubastaYaDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>()
            .AnyAsync(u => u.Email == email && u.State == Domain.Entities.EntityState.ACTIVE, cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
