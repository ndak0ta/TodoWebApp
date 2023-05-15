using Microsoft.AspNetCore.Mvc;
using Todo.Data;
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
    public IEnumerable<Todo.Data.Todo> Get()
    {
        return _todoRepository.GetAll();
    }
}