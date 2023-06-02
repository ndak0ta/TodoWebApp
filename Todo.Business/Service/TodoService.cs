using Todo.Data.Models;
using Todo.Infrastructure.Repositories;

namespace Todo.Business.Service;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll(string userId);
    TodoItem? GetById(int id);
    public void Add(TodoItem todoItem, string userId);
    public void Update(TodoItem todoItem, string userId);
    public void Delete(int todoId, string userId);
    public bool DeleteAllByUserId(string userId);
}

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public IEnumerable<TodoItem> GetAll(string userId)
    {
        var result = _todoRepository.GetAll(int.Parse(userId));

        return result;
    }

    public TodoItem? GetById(int id)
    {
        return _todoRepository.GetById(id);
    }

    public void Add(TodoItem todoItem, string userId)
    {
        if (todoItem == null || userId == null)
            throw new ArgumentException("Eksik veri girişi.");

        todoItem.userId = int.Parse(userId);

        _todoRepository.Add(todoItem);
    }

    public void Update(TodoItem todoItem, string userId)
    {
        if (todoItem == null || userId == null)
            throw new ArgumentException("Eksik veri girişi.");

        todoItem.userId = int.Parse(userId);

        _todoRepository.Update(todoItem);
    }

    public void Delete(int todoId, string userId)
    {
        var todoToDelete = GetById(todoId);

        if (todoToDelete == null || todoToDelete.Id != int.Parse(userId))
            throw new ArgumentException("Ekisk veya hatalı giriş");

        _todoRepository.Delete(todoId);
    }

    public bool DeleteAllByUserId(string userId)
    {
        return _todoRepository.DeleteAllByUserId(int.Parse(userId));
    }
}
