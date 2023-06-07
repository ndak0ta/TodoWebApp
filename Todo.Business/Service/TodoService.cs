using Todo.Data.Models;
using Todo.Infrastructure.Repositories;

namespace Todo.Business.Service;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetAllAsync(string userId);
    Task<TodoItem> GetByIdAsync(int id);
    Task AddAsync(TodoItem todoItem, string userId);
    Task UpdateAsync(TodoItem todoItem, string userId);
    Task DeleteAsync(int todoId, string userId);
    Task DeleteAllByUserIdAsync(string userId);
}

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync(string userId)
    {
        var result = await _todoRepository.GetAllAsync(int.Parse(userId));

        return result;
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(TodoItem todoItem, string userId)
    {
        if (todoItem == null || userId == null)
            throw new ArgumentNullException("Eksik veri girişi.");

        todoItem.userId = int.Parse(userId);

        await _todoRepository.AddAsync(todoItem);
    }

    public async Task UpdateAsync(TodoItem todoItem, string userId)
    {
        if (todoItem == null || userId == null)
            throw new ArgumentNullException("Eksik veri girişi.");

        todoItem.userId = int.Parse(userId);

        await _todoRepository.UpdateAsync(todoItem);
    }

    public async Task DeleteAsync(int todoId, string userId)
    {
        var todoToDelete = await GetByIdAsync(todoId);

        if (todoToDelete == null)
            throw new ArgumentNullException("Eksik giriş");

        if (todoToDelete.userId != int.Parse(userId))
            throw new UnauthorizedAccessException("Yetkisiz işlem.");

        await _todoRepository.DeleteAsync(todoId);
    }

    public async Task DeleteAllByUserIdAsync(string userId)
    {
        await _todoRepository.DeleteAllByUserIdAsync(int.Parse(userId));
    }
}
