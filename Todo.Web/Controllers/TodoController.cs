using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Todo.Data.Models;
using Todo.Business.Service;
using System.Security.Claims;

namespace Todo.Web.Controllers;

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
    public async Task<IActionResult> GetAsync()
    {
        var userId = User.FindFirstValue("userId"); // TODO gelen id verisini int olarak alınacak

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Token alınamadı");

        var todoItems = await _todoService.GetAllAsync(userId);

        return Ok(todoItems);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var todoItem = await _todoService.GetByIdAsync(id);

        if (todoItem == null)
            return BadRequest("Todo bulunamadı");

        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(TodoItem todoItem)
    {
        var userId = User.FindFirstValue("userId");

        if (userId == null)
            throw new ArgumentNullException("Token alınamadı.");

        await _todoService.AddAsync(todoItem, userId);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(TodoItem todoItem)
    {
        var userId = User.FindFirstValue("userId");

        await _todoService.UpdateAsync(todoItem, userId);

        return Ok();
    }

    [HttpDelete("{todoId:int}")]
    public async Task<IActionResult> DeleteAsync(int todoId)
    {
        var userId = User.FindFirstValue("userId");

        await _todoService.DeleteAsync(todoId, userId);

        return Ok();
    }
}
