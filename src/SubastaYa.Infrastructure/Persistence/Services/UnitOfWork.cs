using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;
using SubastaYa.Infrastructure.Persistence.Context;
using SubastaYa.Infrastructure.Persistence.Repositories;

namespace SubastaYa.Infrastructure.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly SubastaYaDbContext _context;
    private readonly IGenericRepository<Auction> _auction = null!;
    private readonly IGenericRepository<AuditLog> _auditLog = null!;
    private readonly IBidRepository _bid = null!;
    private readonly IGenericRepository<Category> _category = null!;
    private readonly ITransactionLedgerRepository _transactionLedger = null!;
    private readonly IUserRepository _user = null!;
    private readonly IWalletRepository _wallet = null!;

    public UnitOfWork(SubastaYaDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Auction> Auction => _auction ?? new GenericRepository<Auction>(_context);

    public IGenericRepository<AuditLog> AuditLog => _auditLog ?? new GenericRepository<AuditLog>(_context);

    public IBidRepository Bid => _bid ?? new BidRepository(_context);

    public IGenericRepository<Category> Category => _category ?? new GenericRepository<Category>(_context);

    public ITransactionLedgerRepository TransactionLedger => _transactionLedger ?? new TransactionLedgerRepository(_context);

    public IUserRepository User => _user ?? new UserRepository(_context);

    public IWalletRepository Wallet => _wallet ?? new WalletRepository(_context);

    public void Dispose() => _context.Dispose();

    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
}
