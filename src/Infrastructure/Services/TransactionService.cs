using Infrastructure.Contexts.Contracts;
using Infrastructure.DTOs;
using Infrastructure.Entities;
using Infrastructure.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly IAppDbContext _dbContext;

    public TransactionService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateTransactionAsync(string transactionBy, decimal amount, CancellationToken cancellationToken = default)
    {
        var transaction = Transaction.Create(transactionBy, amount);
        await _dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return transaction.Id;
    }

    public Task UpdateTransactionAsync(Guid transactionId, string transactionBy, decimal amount, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .Transactions
            .Where(x => x.Id == transactionId)
            .ExecuteUpdateAsync(
                x => x.SetProperty(y => y.TransactionBy, transactionBy)
                    .SetProperty(y => y.Amount, amount), cancellationToken);
    }

    public Task DeleteTransactionAsync(Guid transactionId, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .Transactions
            .Where(x => x.Id == transactionId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public Task<TransactionDto?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .Transactions
            .Select(x => new TransactionDto(x.Id, x.TransactionBy, x.Amount, x.Time))
            .FirstOrDefaultAsync(x => x.Id == transactionId, cancellationToken);
    }

    public async Task<IReadOnlyList<TransactionDto>> GetAllTransactionsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Transactions
            .Select(x => new TransactionDto(x.Id, x.TransactionBy, x.Amount, x.Time))
            .ToListAsync(cancellationToken);
    }
}
