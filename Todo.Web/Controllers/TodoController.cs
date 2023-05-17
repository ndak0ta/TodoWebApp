using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Data.Repositories;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext _context;
    private readonly TodoRepository _todoRepository;

    public TodoController(TodoDbContext context)
    {
        _context = context;
        _todoRepository = new TodoRepository(_context);
    }

    [HttpGet]
    public IActionResult Get()
    {
        var todoItems = _todoRepository.GetAll();

        return Ok(todoItems);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var todoItem = _todoRepository.GetById(id);

        return Ok(todoItem);
    }

    [HttpPost]
    public IActionResult Add([FromBody] JsonElement body)
    {
        var todoItem = JsonSerializer.Deserialize<TodoItem>(body.GetRawText());

        if (todoItem == null)
            return BadRequest("Invalid TodoItem data.");

        var result = _todoRepository.Add(todoItem);

        return result ? Ok() : BadRequest("İşlem tamamlanamadı");
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] JsonElement body)
    {
        var todoItem = JsonSerializer.Deserialize<TodoItem>(body.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        if (todoItem == null || todoItem.Id != id)
            return BadRequest("Todo bulunamadı");

        var result = _todoRepository.Update(id, todoItem);

        return result ? Ok() : BadRequest("İşlem tammalanamadı");
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _todoRepository.Delete(id);

        return result ? Ok() : BadRequest("İşlem tamamlanamadı");
    }
}