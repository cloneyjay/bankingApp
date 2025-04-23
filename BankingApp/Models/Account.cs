using BankingApp.Interfaces;

namespace BankingApp.Models;

public abstract class Account : ITransaction
{
    protected decimal balance;
    public Guid AccountNumber { get; }
    public decimal Balance => balance;
    public List<Transaction> Transactions { get; } = new();

    protected Account(Guid accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        balance = initialBalance;
    }

    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive.");
        }
        balance += amount;
        Transactions.Add(new Transaction(amount, TransactionType.Deposit, AccountNumber));
    }

    public abstract void Withdraw(decimal amount);

    public virtual void ExecuteTransaction(decimal amount)
    {
        if (amount >= 0)
        {
            Deposit(amount);
        }
        else
        {
            Withdraw(-amount);
        }
    }

    public virtual string GetTransactionDetails()
    {
        return $"Account Number: {AccountNumber}, Balance: {Balance:C}";
    }
}