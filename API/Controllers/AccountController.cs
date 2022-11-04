using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Validators.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private IBankService _bankService;

    public AccountController(IBankService bankService)
    {
        _bankService = bankService;
    }

    [HttpGet]
    public IActionResult GetAccounts()
    {
        return Ok(_bankService.GetAccounts());
    }

    [HttpGet("{id}")]
    public IActionResult GetAccount(int id)
    {
        return Ok(_bankService.GetAccount(id));
    }

    [Authorize("AdminPolicy")]
    [HttpPost]
    public IActionResult CreateAccount(PostAccountDTO dto)
    {
        try
        {
            var account = _bankService.CreateAccount(dto);
            return Created("Account/" + account.Id, account);
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
    public IActionResult UpdateAccount(PutAccountDTO dto, int id)
    {
        try
        {
            return Ok(_bankService.UpdateAccount(dto, id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No account found at id " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAccount(int id)
    {
        return Ok(_bankService.DeleteAccount(id));
    }
    
}