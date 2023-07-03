using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Todo.Data.Models;
using Todo.Business.Service;

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
        var userId = User.FindFirstValue("userId"); //  gelen id verisini int olarak alınacak

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Token alınamadı");

        int userIdInt = StringToInt(userId, "Girdi Hatası. Verilen Kullanıcı id bilgisi hatalı");

        var todoItems = await _todoService.GetAllAsync(userIdInt);

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

        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException("Token alınamadı.");

        todoItem.userId = StringToInt(userId, "Girdi Hatası. Verilen Kullanıcı id bilgisi hatalı");

        await _todoService.AddAsync(todoItem);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(TodoItem todoItem)
    {
        var userId = User.FindFirstValue("userId");

        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException("Token alınamadı.");

        todoItem.userId = StringToInt(userId, "Girdi Hatası. Verilen Kullanıcı id bilgisi hatalı");

        await _todoService.UpdateAsync(todoItem);

        return Ok();
    }

    [HttpDelete("{todoId:int}")]
    public async Task<IActionResult> DeleteAsync(int todoId)
    {
        var userId = User.FindFirstValue("userId");

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Token alınamadı");

        int userIdInt = StringToInt(userId, "Girdi Hatası. Verilen Kullanıcı id bilgisi hatalı");

        await _todoService.DeleteAsync(todoId, userIdInt);

        return Ok();
    }

    public static int StringToInt(string value, string exceptionString)
    {
        if (int.TryParse(value, out int valueInt))
            return valueInt;
        else
            throw new ArgumentException(exceptionString);
    }
}
