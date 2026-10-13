using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface IWalletRepository : IGenericRepository<Wallet>
{
    Task<Wallet?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
