namespace BankingApp.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message = "Insufficient funds available.") 
        : base(message)
    {
    }
}

public class CustomerNotFoundException : Exception
{
    public CustomerNotFoundException(string message = "Customer not found.") 
        : base(message)
    {
    }
}

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string message = "Account not found.") 
        : base(message)
    {
    }
}

public class InvalidTransactionException : Exception
{
    public InvalidTransactionException(string message = "Invalid transaction.") 
        : base(message)
    {
    }
}