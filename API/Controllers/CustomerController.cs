using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Validators.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private IBankService _bankService;
    public CustomerController(IBankService bankService)
    {
        _bankService = bankService;
    }

    [HttpGet]
    public IActionResult GetCustomers()
    {
        return Ok(_bankService.GetCustomers());
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        return Ok(_bankService.GetCustomer(id));
    }

    [HttpPost]
    public IActionResult CreateCustomer(PostCustomerDTO dto)
    {
        try
        {
            var customer = _bankService.CreateCustomer(dto);
            return Created("Customer/" + customer.Id, customer);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(PutCustomerDTO dto, int id)
    {
        try
        {
            return Ok(_bankService.UpdateCustomer(dto, id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No customer found at id " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        return Ok(_bankService.DeleteCustomer(id));
        
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("rebuilddb")]
    public void RebuildDB()
    {
        _bankService.RebuildDB();
    }
}