namespace Infrastructure.DTOs;

public sealed record TransactionUpdateDto(Guid Id, string TransactionBy, decimal Amount, byte[] ConcurrencyToken);
