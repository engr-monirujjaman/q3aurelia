namespace Infrastructure.Entities;

public sealed class Transaction
{
    public Guid Id { get; private set; }

    public string TransactionBy { get; private set; } = string.Empty;

    public decimal Amount { get; private set; }
    
    public DateTime Time { get; private set; }

    private Transaction()
    {
        
    }

    public static Transaction Create(string transactionBy, decimal amount) => new Transaction
    {
        TransactionBy = transactionBy,
        Amount = amount,
        Time = DateTime.Now
    };
}
