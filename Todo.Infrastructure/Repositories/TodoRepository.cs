using Todo.Infrastructure.Exceptions;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync(int userId);
    Task<TodoItem> GetByIdAsync(int id);
    Task AddAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(int id);
    Task DeleteAllByUserIdAsync(int userId);
}

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext;

    public TodoRepository(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync(int userId)
    {
        var result = await _todoDbContext.Set<TodoItem>().Where(todo => todo.userId == userId).ToListAsync();

        return result;
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        var result = await _todoDbContext.Set<TodoItem>().FirstOrDefaultAsync(todo => todo.Id == id);

        if (result == null)
            throw new NotFoundException("Kayıt bulunamadı.");

        return result;
    }

    public async Task AddAsync(TodoItem todoItem)
    {
        _todoDbContext.Set<TodoItem>().Add(todoItem);
        await _todoDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        var existingTodoItem = await _todoDbContext.Set<TodoItem>().FirstOrDefaultAsync(t => t.Id == todoItem.Id);

        if (existingTodoItem == null)
            throw new NotFoundException("Üzerine yazılması gereken todo kaydı bulunamadı.");

        existingTodoItem.Header = todoItem.Header ?? existingTodoItem.Header;
        existingTodoItem.Body = todoItem.Body ?? existingTodoItem.Body;

        await _todoDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var todo = await _todoDbContext.Set<TodoItem>().FindAsync(id);

        if (todo == null)
            throw new NotFoundException("Kayıt bulunamadı");

        _todoDbContext.Set<TodoItem>().Remove(todo);
        await _todoDbContext.SaveChangesAsync();
    }

    public async Task DeleteAllByUserIdAsync(int userId)
    {
        var recordsToDelete = await _todoDbContext.Set<TodoItem>().Where(t => t.userId == userId).ToListAsync();

        if (recordsToDelete == null)
            throw new NotFoundException("Kayıt bulunamadı");

        _todoDbContext.TodoItem?.RemoveRange(recordsToDelete);
        await _todoDbContext.SaveChangesAsync();
    }
}