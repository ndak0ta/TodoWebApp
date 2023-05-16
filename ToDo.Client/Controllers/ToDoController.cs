using Microsoft.AspNetCore.Mvc;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Data.Repositories;

namespace Todo.Client.Controllers;

[ApiController]
[Route("[controller]")]
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
        var todos = _todoRepository.GetAll();
        
        return Ok(todos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var todo = _todoRepository.GetById(id);

        return Ok(todo);
    }

    [HttpPost]
    public IActionResult Add(TodoItem todoItem)
    {
        _todoRepository.Add(todoItem);
        
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, TodoItem todoItem)
    {
        var entryToUpdate = _todoRepository.GetById(id);
        
        _todoRepository.Update(todoItem);

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _todoRepository.Delete(id);

        return Ok();
    }
}