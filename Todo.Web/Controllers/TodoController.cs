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
        try
        {
            var userId = User.FindFirstValue("userId");

            if (userId == null)
                throw new ArgumentException("Token alınamadı.");

            _todoService.Add(todoItem, userId);

            return Ok();
        }
        catch
        {
            return BadRequest("Beklenmeyen bir hata oluştu.");
        }
    }


    [HttpPut]
    public IActionResult Update(TodoItem todoItem)
    {
        try
        {
            var userId = User.FindFirstValue("userId");

            _todoService.Update(todoItem, userId);

            return Ok();
        }
        catch
        {
            return BadRequest("Beklenmeyen bir hata oluştu.");
        }
    }

    [HttpDelete("{todoId:int}")]
    public IActionResult Delete(int todoId)
    {
        try
        {
            var userId = User.FindFirstValue("userId");

            _todoService.Delete(todoId, userId);

            return Ok();
        }
        catch
        {
            return BadRequest("Beklenmeyen bir hata oluştu.");
        }
    }
}