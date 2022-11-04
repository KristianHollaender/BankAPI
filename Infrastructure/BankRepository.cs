using Application.Interfaces;
using Application;
using Domain;

namespace Infrastructure;

public class BankRepository : IBankRepository
{
    private DatabaseContext _context;

    public BankRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public List<Customer> GetCustomers()
    {
        return _context.Customers.ToList();

    }
    
    public Customer GetCustomer(int id)
    {
        var customer = _context.Customers.FirstOrDefault(customer => customer.Id == id);
        customer.Accounts = GetAccounts().Where(x => x.CustomerId == customer.Id).ToList();
        return customer;
    }

    public List<Account> GetAccounts()
    {
        return _context.Accounts.ToList();

    }
    
    public Account GetAccount(int id)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.Id == id);
        account.Customer = GetCustomer(account.CustomerId);
        return account;  
    }

 
    public Customer DeleteCustomer(int CustomerId)
    {
        var cust = _context.Customers.FirstOrDefault(customer => customer.Id == CustomerId);
        _context.Customers.Remove(cust);
        _context.SaveChanges();
        return cust;
    }

    public Customer CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public Customer UpdateCustomer(Customer customer, int id)
    {
        var cust = GetCustomer(id);
        if (cust.Id == id)
        {
            cust.FirstName = customer.FirstName;
            cust.LastName = customer.LastName;
            _context.Update(cust);
            _context.SaveChanges();
        } 
        return customer;
    }
    
    public Account DeleteAccount(int AccountId)
    {
        var acc = _context.Accounts.FirstOrDefault(account => account.Id == AccountId);
        _context.Accounts.Remove(acc);
        _context.SaveChanges();
        return acc;
    }

    public Account CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public Account UpdateAccount(Account account, int id)
    {
        var acc = _context.Accounts.FirstOrDefault(account => account.Id == id);
        if (acc.Id == id)
        {
            acc.Id = account.Id;
            acc.AccountName = account.AccountName;
            acc.Amount = account.Amount;
            acc.CustomerId = account.CustomerId;
            _context.Update(acc);
            _context.SaveChanges();
        }
        return acc;
    }
}