using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Todo.Data.Models;
using Todo.Business.Service;
using System.Security.Claims;

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
        var userId = User.FindFirstValue("userId");

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Token alınamadı");

        var todoItems = _todoService.GetAll(userId);

        return Ok(todoItems);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var todoItem = _todoService.GetById(id);

        if (todoItem == null)
            return BadRequest("Todo bulunamadı");

        return Ok(todoItem);
    }

    [HttpPost]
    public IActionResult Add(TodoItem todoItem)
    {
        var userId = User.FindFirstValue("userId");

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Token alınamadı");

        var result = _todoService.Add(todoItem, userId);

        return result ? Ok() : BadRequest("İşlem tamamlanamadı");
    }

    [HttpPut]
    public IActionResult Update(TodoItem todoItem)
    {
        var userId = User.FindFirstValue("userId");

        var result = _todoService.Update(todoItem, userId);

        return result ? Ok() : BadRequest("İşlem tammalanamadı");
    }

    [HttpDelete("{todoId:int}")]
    public IActionResult Delete(int todoId)
    {
        var userId = User.FindFirstValue("userId");

        var result = _todoService.Delete(todoId, userId);

        return result ? Ok() : BadRequest("İşlem tamamlanamadı");
    }
}