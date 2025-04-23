using BankingApp.Models;
using BankingApp.Exceptions;

namespace BankingApp.Services;

public class BankService
{
    private readonly Dictionary<Guid, Customer> _customers = new();
    private readonly Dictionary<Guid, Account> _accounts = new();

    public Customer CreateCustomer(string name, string contactInfo)
    {
        var customer = new Customer(name, contactInfo);
        _customers[customer.CustomerID] = customer;
        return customer;
    }

    public Account CreateAccount(Guid customerId, string accountType, decimal initialBalance)
    {
        if (!_customers.TryGetValue(customerId, out var customer))
            throw new ArgumentException("Customer not found.");

        Account account = accountType.ToLower() switch
        {
            "savings" => new SavingsAccount(Guid.NewGuid(), initialBalance),
            "current" => new CurrentAccount(Guid.NewGuid(), initialBalance),
            _ => throw new ArgumentException("Invalid account type")
        };

        _accounts[account.AccountNumber] = account;
        customer.Accounts.Add(account);
        return account;
    }

    public Account GetAccount(Guid accountNumber)
    {
        if (!_accounts.TryGetValue(accountNumber, out var account))
            throw new ArgumentException("Account not found.");
        return account;
    }

    public void processTransaction(Guid accountNumber, decimal amount, TransactionType type)
    {
        var account = GetAccount(accountNumber);
        
        try
        {
            switch (type)
            {
                case TransactionType.Deposit:
                    account.Deposit(amount);
                    break;
                case TransactionType.Withdrawal:
                    account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentException("Invalid transaction type");
            }
        }
        catch (InsufficientFundsException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error processing transaction: {ex.Message}");
        }
    }

    public List<Transaction> GetAccountTransactions(Guid accountNumber)
    {
        var account = GetAccount(accountNumber);
        return account.Transactions;
    }

    public Customer GetCustomer(Guid customerId)
    {
        if (!_customers.TryGetValue(customerId, out var customer))
            throw new ArgumentException("Customer not found.");
        return customer;
    }

    public List<Account> GetCustomerAccounts(Guid customerId)
    {
        var customer = GetCustomer(customerId);
        return customer.Accounts;
    }
}