using Microsoft.EntityFrameworkCore;
using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;

namespace SubastaYa.Infrastructure.Persistence.Repositories;

public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
{
    public WalletRepository(SubastaYaDbContext context) : base(context)
    {
    }

    public async Task<Wallet?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Wallet>()
            .FirstOrDefaultAsync(w => w.UserId == userId && w.State == Domain.Entities.EntityState.ACTIVE, cancellationToken);
    }
}
