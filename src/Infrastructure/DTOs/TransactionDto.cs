namespace Infrastructure.DTOs;

public sealed record TransactionDto(Guid Id, string TransactionBy, decimal Amount, DateTime Time, byte[] ConcurrencyToken);
