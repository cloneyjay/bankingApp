namespace BankingApp.Models;

public class Customer
{
    public Guid CustomerID { get; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public List<Account> Accounts { get; } = new();
    
    public Customer(string name, string contactInfo)
    {
        CustomerID = Guid.NewGuid();
        Name = name;
        ContactInfo = contactInfo;
    }

    public Customer(Guid customerId, string name, string contactInfo)
    {
        CustomerID = customerId;
        Name = name;
        ContactInfo = contactInfo;
    }
}