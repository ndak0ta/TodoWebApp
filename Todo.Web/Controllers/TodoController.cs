using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Business.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [AllowAnonymous]
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        var todoItems = _todoService.GetAll();

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
        var result = _todoService.Add(body);

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