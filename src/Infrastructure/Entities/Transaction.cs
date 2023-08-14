using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public sealed class Transaction
{
    public Guid Id { get; private set; }

    public string TransactionBy { get; private set; } = string.Empty;

    public decimal Amount { get; private set; }

    public DateTime Time { get; private set; }

    [ConcurrencyCheck]
    public byte[] ConcurrencyToken { get; private set; } = Array.Empty<byte>();

    private Transaction()
    {

    }

    public void Update(string transactionBy, decimal amount)
        => (TransactionBy, Amount, ConcurrencyToken) = (transactionBy, amount, Guid.NewGuid().ToByteArray());

    public static Transaction Create(string transactionBy, decimal amount) => new Transaction
    {
        TransactionBy = transactionBy,
        Amount = amount,
        Time = DateTime.Now
    };
}
