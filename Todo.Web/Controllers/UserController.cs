using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Business.Service;


namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
        _userService = userService;
	}

	[HttpPost]
	public void Add(User user)
	{
		_userService.Add(user);
	}

	[HttpDelete]
	public void Delete()
	{
		string userId = User.FindFirstValue("userId");

        _userService.Delete(userId);
	}
}
