using BankingApp.Exceptions;

namespace BankingApp.Models;

public class CurrentAccount : Account
{
    private readonly decimal overdraftLimit = -1000m;
    
    public CurrentAccount(Guid accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount cannot be less or equal to zero");
        if (Balance - amount < overdraftLimit)
            throw new InsufficientFundsException("Overdraft limit exceeded");
        
        base.balance -= amount;
        Transactions.Add(new Transaction(amount, TransactionType.Withdrawal, AccountNumber));
    }
}