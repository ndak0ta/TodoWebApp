using Todo.Infrastructure.Exceptions;
using Todo.Data.Contexts;
using Todo.Data.Models;

namespace Todo.Infrastructure.Repositories;

public interface ITodoRepository
{
    public IEnumerable<TodoItem> GetAll(int userId);
    public TodoItem? GetById(int id);
    public void Add(TodoItem todoItem);
    public void Update(TodoItem todoItem);
    public void Delete(int id);
    public bool DeleteAllByUserId(int userId);
}

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext;

    public TodoRepository(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public IEnumerable<TodoItem> GetAll(int userId)
    {
        var result = _todoDbContext.Set<TodoItem>().Where(todo => todo.userId == userId).ToList();

        return result;
    }

    public TodoItem? GetById(int id)
    {
        var result = _todoDbContext.Set<TodoItem>().FirstOrDefault(todo => todo.Id == id);

        return result;
    }

    public void Add(TodoItem todoItem)
    { 
        _todoDbContext.Set<TodoItem>().Add(todoItem);
        _todoDbContext.SaveChanges();
    }

    public void Update(TodoItem todoItem)
    {
        var existingTodoItem = _todoDbContext.TodoItem?.FirstOrDefault(t => t.Id == todoItem.Id);

        if (existingTodoItem == null)
            throw new NotFoundException("Üzerine yazılması gereken todo kaydı bulunamadı.");

        existingTodoItem.Header = todoItem.Header ?? existingTodoItem.Header;
        existingTodoItem.Body = todoItem.Body ?? existingTodoItem.Body;

        _todoDbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var todo = _todoDbContext.Set<TodoItem>().Find(id);

        if (todo == null)
            throw new NotFoundException("Kayıt bulunamadı");

        _todoDbContext.Set<TodoItem>().Remove(todo);
        _todoDbContext.SaveChanges();
    }

    public bool DeleteAllByUserId(int userId)
    {
        var recordsToDelete = _todoDbContext.Set<TodoItem>().Where(t => t.userId == userId);

        if (recordsToDelete == null)
            return false;

        _todoDbContext.TodoItem?.RemoveRange(recordsToDelete);
        _todoDbContext.SaveChanges();

        return true;
    }
}
