using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Domain;


namespace Application;

public class BankService : IBankService
{
    private IBankRepository _repository;
    private IMapper _mapper;
    private IValidator<PutCustomerDTO> _validationCustomerPut;
    private IValidator<PostCustomerDTO> _validationCustomerPost;
    private IValidator<PutAccountDTO> _validationAccountPut;
    private IValidator<PostAccountDTO> _validationAccountPost;

    public BankService(IBankRepository repository, 
        IMapper mapper, 
        IValidator<PutCustomerDTO> validationCustomerPut, 
        IValidator<PostCustomerDTO> validationCustomerPost, 
        IValidator<PutAccountDTO> validationAccountPut, 
        IValidator<PostAccountDTO> validationAccountPost)
    {
        _repository = repository;
        _mapper = mapper;
        _validationCustomerPut = validationCustomerPut;
        _validationCustomerPost = validationCustomerPost;
        _validationAccountPut = validationAccountPut;
        _validationAccountPost = validationAccountPost;
    }

    public Customer CreateCustomer(PostCustomerDTO customer)
    {
        var validate = _validationCustomerPost.Validate(customer);
        if (!validate.IsValid)
            throw new ValidationException(validate.ToString());
        
        return _repository.CreateCustomer(_mapper.Map<Customer>(customer));
    }

    public Account CreateAccount(PostAccountDTO account)
    {
        var validate = _validationAccountPost.Validate(account);
        if (!validate.IsValid)
            throw new ValidationException(validate.ToString());
        return _repository.CreateAccount(_mapper.Map<Account>(account));
    }

    public List<Customer> GetCustomers()
    {
        return _repository.GetCustomers();
    }

    public List<Account> GetAccounts()
    {
        return _repository.GetAccounts();
    }

    public Customer GetCustomer(int id)
    {
        return _repository.GetCustomer(id);
    }

    public Account GetAccount(int id)
    {
        return _repository.GetAccount(id);
    }

    public Customer UpdateCustomer(PutCustomerDTO customer, int id)
    {
        if (id != customer.Id)
            throw new ValidationException("ID in the body and route are different");
        var validate = _validationCustomerPut.Validate(customer);
        if (!validate.IsValid)
            throw new ValidationException(validate.ToString());
        return _repository.UpdateCustomer(_mapper.Map<Customer>(customer), id);
    }

    public Account UpdateAccount(PutAccountDTO account, int id)
    {
        if (id != account.id)
            throw new ValidationException("ID in the body and route are different");
        var validate = _validationAccountPut.Validate(account);
        if (!validate.IsValid)
            throw new ValidationException(validate.ToString());
        return _repository.UpdateAccount(_mapper.Map<Account>(account), id);
    }

    public Customer DeleteCustomer(int customerId)
    {
        return _repository.DeleteCustomer(customerId);
    }

    public Account DeleteAccount(int accountId)
    {
        return _repository.DeleteAccount(accountId);
    }

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }
}