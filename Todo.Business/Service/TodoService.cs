using System.Text.Json;
using Todo.Data.Models;
using Todo.Infrastructure.Repositories;

namespace Todo.Business.Service;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll();
    TodoItem GetById(int id);
    bool Add(JsonElement body);
    bool Update(int id, JsonElement body);
    bool Delete(int id);
}

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public IEnumerable<TodoItem> GetAll()
    {
        return _todoRepository.GetAll();
    }

    public TodoItem GetById(int id)
    {
        return _todoRepository.GetById(id);
    }

    public bool Add(JsonElement body)
    {
        var todoItem = JsonSerializer.Deserialize<TodoItem>(body.GetRawText());

        if (todoItem == null)
            return false;

        return _todoRepository.Add(todoItem);
    }

    public bool Update(int id, JsonElement body)
    {
        var todoItem = JsonSerializer.Deserialize<TodoItem>(body.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        if (todoItem == null || todoItem.Id != id)
            return false;

        return _todoRepository.Update(id, todoItem);
    }

    public bool Delete(int id)
    {
        return _todoRepository.Delete(id);
    }
}
