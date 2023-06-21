using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Models;
using Todo.Business.Service;

namespace Todo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(User user)
        {
            await _userService.AddAsync(user);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(User user)
        { 
            await _userService.DeleteAsync(user);

            return Ok();
        }
    }
}
