using BankingApp.Models;
using BankingApp.Services;
using BankingApp.Utilities;
using BankingApp.Exceptions;

namespace BankingApp
{
    class Program
    {
        private static readonly BankService _bankService = new();

        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleHelper.DisplayMenu();
                string choice = Console.ReadLine() ?? string.Empty;
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateCustomer();
                            break;
                        case "2":
                            CreateAccount();
                            break;
                        case "3":
                            PerformDeposit();
                            break;
                        case "4":
                            PerformWithdrawal();
                            break;
                        case "5":
                            ApplyInterest();
                            break;
                        case "6":
                            CheckBalance();
                            break;
                        case "7":
                            ViewTransactionHistory();
                            break;
                        case "8":
                            return;
                        default:
                            ConsoleHelper.DisplayError("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.DisplayError(ex.Message);
                }
                
                ConsoleHelper.WaitForKeyPress();
            }
        }

        private static void CreateCustomer()
        {
            var name = ConsoleHelper.GetUserInput("Enter customer name: ", input =>
                string.IsNullOrWhiteSpace(input) 
                    ? (false, "Name cannot be empty.", string.Empty)
                    : (true, string.Empty, input));

            var contactInfo = ConsoleHelper.GetUserInput("Enter contact info (email): ", input =>
                string.IsNullOrWhiteSpace(input) 
                    ? (false, "Contact info cannot be empty.", string.Empty)
                    : (true, string.Empty, input));

            var customer = _bankService.CreateCustomer(name, contactInfo);
            ConsoleHelper.DisplaySuccess($"Customer created successfully! ID: {customer.CustomerID}");
        }

        private static void CreateAccount()
        {
            var customerId = ConsoleHelper.GetUserInput("Enter customer ID: ", input =>
            {
                if (Guid.TryParse(input, out Guid id))
                    return (true, string.Empty, id);
                return (false, "Invalid customer ID format.", Guid.Empty);
            });

            var accountType = ConsoleHelper.GetUserInput("Enter account type (savings/current): ", input =>
            {
                var type = input.ToLower();
                return type is "savings" or "current" 
                    ? (true, string.Empty, type)
                    : (false, "Invalid account type. Please enter 'savings' or 'current'.", string.Empty);
            });

            var initialBalance = ConsoleHelper.GetAmount("Enter initial balance: ");
            
            var account = _bankService.CreateAccount(customerId, accountType, initialBalance);
            ConsoleHelper.DisplaySuccess($"Account created successfully! Account Number: {account.AccountNumber}");
        }

        private static Account GetAccount()
        {
            var accountNumber = ConsoleHelper.GetUserInput("Enter account number: ", input =>
            {
                if (Guid.TryParse(input, out Guid id))
                    return (true, string.Empty, id);
                return (false, "Invalid account number format.", Guid.Empty);
            });

            return _bankService.GetAccount(accountNumber);
        }

        private static void PerformDeposit()
        {
            var account = GetAccount();
            var amount = ConsoleHelper.GetAmount();
            _bankService.processTransaction(account.AccountNumber, amount, TransactionType.Deposit);
            ConsoleHelper.DisplaySuccess($"Successfully deposited {amount:C}. New balance: {account.Balance:C}");
        }

        private static void PerformWithdrawal()
        {
            var account = GetAccount();
            var amount = ConsoleHelper.GetAmount();
            _bankService.processTransaction(account.AccountNumber, amount, TransactionType.Withdrawal);
            ConsoleHelper.DisplaySuccess($"Successfully withdrew {amount:C}. New balance: {account.Balance:C}");
        }

        private static void ApplyInterest()
        {
            var account = GetAccount();
            if (account is SavingsAccount savings)
            {
                savings.ApplyInterest();
                ConsoleHelper.DisplaySuccess($"Interest applied successfully! New balance: {account.Balance:C}");
            }
            else
            {
                ConsoleHelper.DisplayError("Interest can only be applied to savings accounts.");
            }
        }

        private static void CheckBalance()
        {
            var account = GetAccount();
            ConsoleHelper.DisplaySuccess($"Current balance: {account.Balance:C}");
        }

        private static void ViewTransactionHistory()
        {
            var account = GetAccount();
            var transactions = _bankService.GetAccountTransactions(account.AccountNumber);
            
            Console.WriteLine("\nTransaction History:");
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
