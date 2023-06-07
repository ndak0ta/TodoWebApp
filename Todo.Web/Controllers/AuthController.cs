using Microsoft.AspNetCore.Mvc;
using Todo.Data.Models;
using Todo.Business.Service;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        var token = await _authService.LoginAsync(user);
    
        if (token == null)
            return BadRequest("Giriş yapılamadı");
    
        return Ok(new { token });
    }
    
}