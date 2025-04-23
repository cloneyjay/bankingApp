using BankingApp.Exceptions;

namespace BankingApp.Models;

public class SavingsAccount : Account
{
    private const decimal AnnualInterestRate = 0.03m; // 3% annual interest rate

    public SavingsAccount(Guid accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount cannot be less or equal to zero");
        if (Balance - amount < 0)
            throw new InsufficientFundsException("Insufficient funds in savings account.");
        
        base.balance -= amount;
        Transactions.Add(new Transaction(amount, TransactionType.Withdrawal, AccountNumber));
    }

    public void ApplyInterest()
    {
        decimal interest = Balance * AnnualInterestRate;
        Deposit(interest);
    }
}