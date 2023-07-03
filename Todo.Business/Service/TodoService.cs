using Todo.Data.Models;
using Todo.Infrastructure.Repositories;

namespace Todo.Business.Service;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetAllAsync(int userId);
    Task<TodoItem> GetByIdAsync(int id);
    Task AddAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(int todoId, int userId);
    Task DeleteAllByUserIdAsync(User user);
}

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync(int userId)
    {
        var result = await _todoRepository.GetAllAsync(userId);

        return result;
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(TodoItem todoItem)
    {
        if (todoItem == null)
            throw new ArgumentNullException("Eksik veri girişi.");

        await _todoRepository.AddAsync(todoItem);
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        await _todoRepository.UpdateAsync(todoItem);
    }

    public async Task DeleteAsync(int todoId, int userId)
    {
        var todoToDelete = await GetByIdAsync(todoId);

        if (todoToDelete == null)
            throw new ArgumentNullException("Eksik giriş");

        if (todoToDelete.userId != userId)
            throw new UnauthorizedAccessException("Yetkisiz işlem.");

        await _todoRepository.DeleteAsync(todoId);
    }

    public async Task DeleteAllByUserIdAsync(User user)
    {
        await _todoRepository.DeleteAllByUserIdAsync(user);
    }
}
