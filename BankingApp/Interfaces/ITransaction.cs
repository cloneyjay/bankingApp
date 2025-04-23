namespace BankingApp.Interfaces;

public interface ITransaction
{
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    void ExecuteTransaction(decimal amount);
    string GetTransactionDetails();
}