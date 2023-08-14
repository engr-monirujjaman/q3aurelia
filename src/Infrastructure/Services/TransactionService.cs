using Infrastructure.Contexts.Contracts;
using Infrastructure.DTOs;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
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

    public async Task<Guid> CreateTransactionAsync(TransactionCreateDto dto, CancellationToken cancellationToken = default)
    {
        var transaction = Transaction.Create(dto.TransactionBy, dto.Amount);
        await _dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return transaction.Id;
    }

    public async Task UpdateTransactionAsync(TransactionUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Transactions
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (transaction is null)
        {
            throw new NotFoundException();
        }

        if (!transaction.ConcurrencyToken.SequenceEqual(dto.ConcurrencyToken))
        {
            throw new ConcurrencyConflictException();
        }
        
        transaction.Update(dto.TransactionBy, dto.Amount);
        await _dbContext.SaveChangesAsync(cancellationToken);
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
            .Select(x => new TransactionDto(x.Id, x.TransactionBy, x.Amount, x.Time, x.ConcurrencyToken))
            .FirstOrDefaultAsync(x => x.Id == transactionId, cancellationToken);
    }

    public async Task<IReadOnlyList<TransactionDto>> GetAllTransactionsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Transactions
            .Select(x => new TransactionDto(x.Id, x.TransactionBy, x.Amount, x.Time, x.ConcurrencyToken))
            .ToListAsync(cancellationToken);
    }
}
