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
	public IActionResult Add(User user)
	{
		var result = _userService.Add(user);

		return result ? Ok() : BadRequest("İşlem gerçekleştirilemedi");
	}

	[HttpDelete]
	public IActionResult Delete()
	{
		string userId = User.FindFirstValue("userId");

        var result = _userService.Delete(userId);

		return result ? Ok() : BadRequest("işlem gerçekleştirilemedi");
	}
}
