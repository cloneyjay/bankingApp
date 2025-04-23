namespace BankingApp.Models;

public enum TransactionType
{
    Deposit,
    Withdrawal
}

public class Transaction
{
    public decimal Amount { get; }
    public TransactionType Type { get; }
    public DateTime Timestamp { get; }
    public Guid AccountNumber { get; }

    public Transaction(decimal amount, TransactionType type, Guid accountNumber)
    {
        Amount = amount;
        Type = type;
        Timestamp = DateTime.Now;
        AccountNumber = accountNumber;
    }

    public override string ToString()
    {
        var operation = Type == TransactionType.Deposit ? "+" : "-";
        return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {operation}{Amount:C} (Account: {AccountNumber})";
    }
}