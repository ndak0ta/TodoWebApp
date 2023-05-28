using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Business.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Security.Principal;

namespace Todo.Web.Controllers;

[AllowAnonymous]
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            return BadRequest("Kullanıcı bilgisine erişilemedi");

        var todoItems = _todoService.GetAll(identity);

        return Ok(todoItems);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var todoItem = _todoService.GetById(id);

        return Ok(todoItem);
    }

    [HttpPost]
    public IActionResult Add([FromBody] JsonElement body)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            return BadRequest("Kullanıcı bilgisine erişilemedi");

        var result = _todoService.Add(body, identity);

        return result ? Ok() : BadRequest("İşlem tamamlanamadı");
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] JsonElement body)
    { 
        var result = _todoService.Update(id, body);

        return result ? Ok() : BadRequest("İşlem tammalanamadı");
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _todoService.Delete(id);

        return result ? Ok() : BadRequest("İşlem tamamlanamadı");
    }
}