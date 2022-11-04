using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Validators.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly IAuthenticationService _authentication;

    public AuthController(IAuthenticationService authentication)
    {
        _authentication = authentication;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public ActionResult Login(LoginAndRegisterDTO dto)
    {
        try
        {
            return Ok(_authentication.Login(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpPost]
    [Route("register")]
    public ActionResult Register(LoginAndRegisterDTO dto)
    {
        try
        {
            return Ok(_authentication.Register(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}