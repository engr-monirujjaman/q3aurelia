using Infrastructure.DTOs;

namespace Infrastructure.Services.Contracts;

public interface ITransactionService
{
    Task<Guid> CreateTransactionAsync(string transactionBy, decimal amount, CancellationToken cancellationToken = default);

    Task UpdateTransactionAsync(Guid transactionId, string transactionBy, decimal amount, CancellationToken cancellationToken = default);

    Task DeleteTransactionAsync(Guid transactionId, CancellationToken cancellationToken = default);

    Task<TransactionDto?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TransactionDto>> GetAllTransactionsAsync(CancellationToken cancellationToken = default);
}
