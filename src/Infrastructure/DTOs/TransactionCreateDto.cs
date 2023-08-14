namespace Infrastructure.DTOs;

public sealed record TransactionCreateDto(string TransactionBy, decimal Amount);