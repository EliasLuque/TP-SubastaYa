using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}
