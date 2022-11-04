namespace Domain;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Account>? Accounts { get; set; }
}

public class Account
{
    public int Id { get; set; }
    public string AccountName { get; set; }
    public float Amount { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}