using Microsoft.EntityFrameworkCore;
using Todo.Data.Contexts;
using Todo.Data.Models;

namespace Todo.Infrastructure.Repositories;

public interface ITodoRepository
{
    public IEnumerable<TodoItem> GetAll();
    public TodoItem GetById(int id);
    public bool Add(TodoItem todoItem);
    public bool Update(int id, TodoItem todoItem);
    public bool Delete(int id);
}

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _dbContext;

    public TodoRepository(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<TodoItem> GetAll()
    {
        var result = _dbContext.Set<TodoItem>().ToList();
        return result;
    }

    public TodoItem GetById(int id)
    {
        var result = _dbContext.Set<TodoItem>().FirstOrDefault(p => p.Id == id);

        if (result == null)
            throw new ArgumentException(nameof(result) + " Böyle bir todo yok");

        return result;
    }

    public bool Add(TodoItem todoItem)
    { 
        try
        {
            _dbContext.Set<TodoItem>().Add(todoItem);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Update(int id, TodoItem todoItem)
    {
        var existingTodoItem = _dbContext.TodoItem?.FirstOrDefault(t => t.Id == id);

        if (existingTodoItem == null)
        {
            return false;
        }

        existingTodoItem.Header = todoItem.Header ?? existingTodoItem.Header;
        existingTodoItem.Body = todoItem.Body ?? existingTodoItem.Body;

        _dbContext.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var todo = _dbContext.Set<TodoItem>().Find(id);

        if (todo == null)
            return false;

        _dbContext.Set<TodoItem>().Remove(todo);
        _dbContext.SaveChanges();

        return true;
    }
}
