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
    private readonly IGenericRepository<Bid> _bid = null!;
    private readonly IGenericRepository<Category> _category = null!;
    private readonly IGenericRepository<TransactionLedger> _transactionLedger = null!;
    private readonly IGenericRepository<User> _user = null!;
    private readonly IGenericRepository<Wallet> _wallet = null!;

    public UnitOfWork(SubastaYaDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Auction> Auction => _auction ?? new GenericRepository<Auction>(_context);

    public IGenericRepository<AuditLog> AuditLog => _auditLog ?? new GenericRepository<AuditLog>(_context);

    public IGenericRepository<Bid> Bid => _bid ?? new GenericRepository<Bid>(_context);

    public IGenericRepository<Category> Category => _category ?? new GenericRepository<Category>(_context);

    public IGenericRepository<TransactionLedger> TransactionLedger => _transactionLedger ?? new GenericRepository<TransactionLedger>(_context);

    public IGenericRepository<User> User => _user ?? new GenericRepository<User>(_context);

    public IGenericRepository<Wallet> Wallet => _wallet ?? new GenericRepository<Wallet>(_context);

    public void Dispose() => _context.Dispose();

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
