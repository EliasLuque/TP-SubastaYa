using SubastaYa.Aplication.Interface.Persistence;

namespace SubastaYa.Aplication.Interface.Services;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}
