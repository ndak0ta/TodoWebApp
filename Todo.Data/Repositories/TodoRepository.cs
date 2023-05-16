using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Data.Repositories;

public class TodoRepository
{
    private readonly DbContext _dbContext;

    public TodoRepository(DbContext dbContext)
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
    
    public void Update(TodoItem todoItem)
    {
        _dbContext.Set<TodoItem>().Update(todoItem);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var todo = _dbContext.Set<TodoItem>().Find(id);
        _dbContext.Set<TodoItem>().Remove(todo);
        _dbContext.SaveChanges();
    }
}