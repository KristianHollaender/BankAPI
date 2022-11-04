using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IBankService
{
    List<Customer> GetCustomers();
    Customer GetCustomer(int id);
    Customer DeleteCustomer(int CustomerId);
    Customer CreateCustomer(PostCustomerDTO customer);
    Customer UpdateCustomer(PutCustomerDTO customer, int id);
    
    List<Account> GetAccounts();
    Account GetAccount(int id);
    Account DeleteAccount(int AccountId);
    Account CreateAccount(PostAccountDTO account);
    Account UpdateAccount(PutAccountDTO account, int id);

    void RebuildDB();
}