using Application;
using Domain;

namespace Application.Interfaces;

public interface IBankRepository
{
    Customer GetCustomer(int id);
    List<Customer> GetCustomers();
    Customer DeleteCustomer(int CustomerId);
    Customer CreateCustomer(Customer customer);
    Customer UpdateCustomer(Customer customer, int id);

    Account GetAccount(int id);
    List<Account> GetAccounts();
    Account DeleteAccount(int AccountId);
    Account CreateAccount(Account account);
    Account UpdateAccount(Account account, int id);
    void RebuildDB();
}