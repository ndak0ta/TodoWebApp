using Todo.Data.Models;
using Todo.Infrastructure.Repositories;

namespace Todo.Business.Service;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll(string userId);
    TodoItem? GetById(int id);
    bool Add(TodoItem todoItem, string userId);
    bool Update(TodoItem todoItem, string userId);
    bool Delete(int todoId, string userId);
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

    public bool Add(TodoItem todoItem, string userId)
    {
        if (todoItem == null || userId == null)
            return false;

        todoItem.userId = int.Parse(userId);

        return _todoRepository.Add(todoItem);
    }

    public bool Update(TodoItem todoItem, string userId)
    {
        if (todoItem == null || userId == null)
            return false;

        todoItem.userId = int.Parse(userId);

        return _todoRepository.Update(todoItem);
    }

    public bool Delete(int todoId, string userId)
    {
        var todoToDelete = GetById(todoId);

        if (todoToDelete == null || todoToDelete.Id != int.Parse(userId))
            return false;

        return _todoRepository.Delete(todoId);
    }

    public bool DeleteAllByUserId(string userId)
    {
        return _todoRepository.DeleteAllByUserId(int.Parse(userId));
    }
}
