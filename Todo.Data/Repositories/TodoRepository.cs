using Microsoft.EntityFrameworkCore;
using Todo.Data.Contexts;
using Todo.Data.Models;

namespace Todo.Data.Repositories;

public class TodoRepository
{
    private readonly TodoDbContext _dbContext;

    public TodoRepository(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<TodoItem> GetAll()
    {
        return _dbContext.Set<TodoItem>().ToList();
    }

    public TodoItem GetById(int id)
    {
        var result = _dbContext.Set<TodoItem>().FirstOrDefault(p => p.Id == id);

        if (result == null)
            throw new ArgumentException(nameof(result) + " Böyle bir todo yok");

        return result;
    }

    public void Add(TodoItem todoItem)
    {
        _dbContext.Set<TodoItem>().Add(todoItem);
        _dbContext.SaveChanges();
    }

    public bool Update(int id, TodoItem todoItem)
    {
        var existingTodoItem = _dbContext.Todo.FirstOrDefault(t => t.Id == id);

        if (existingTodoItem == null)
        {
            return false;
        }

        existingTodoItem.Header = todoItem.Header ?? existingTodoItem.Header;
        existingTodoItem.Body = todoItem.Body ?? existingTodoItem.Body;

        _dbContext.SaveChanges();

        return true;
    }

    public void Delete(int id)
    {
        var todo = _dbContext.Set<TodoItem>().Find(id);
        _dbContext.Set<TodoItem>().Remove(todo);
        _dbContext.SaveChanges();
    }
}