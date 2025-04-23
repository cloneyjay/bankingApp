namespace BankingApp.Utilities;

public static class ConsoleHelper
{
    public static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Banking System Menu ===");
        Console.WriteLine("1. Create New Customer");
        Console.WriteLine("2. Create Account for Customer");
        Console.WriteLine("3. Make Deposit");
        Console.WriteLine("4. Make Withdrawal");
        Console.WriteLine("5. Apply Interest (Savings Account)");
        Console.WriteLine("6. Check Balance");
        Console.WriteLine("7. View Transaction History");
        Console.WriteLine("8. Exit");
        Console.Write("\nEnter your choice (1-8): ");
    }

    public static T GetUserInput<T>(string prompt, Func<string, (bool isValid, string error, T value)> parser)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            var (isValid, error, value) = parser(input);
            
            if (isValid)
                return value;
            
            Console.WriteLine($"Error: {error}");
        }
    }

    public static decimal GetAmount(string prompt = "Enter amount: ")
    {
        return GetUserInput(prompt, input =>
        {
            if (decimal.TryParse(input, out decimal amount) && amount > 0)
                return (true, string.Empty, amount);
            return (false, "Please enter a valid positive amount.", 0);
        });
    }

    public static void WaitForKeyPress()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ResetColor();
    }

    public static void DisplaySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}