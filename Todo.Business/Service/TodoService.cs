using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Todo.Data.Models;
using Todo.Infrastructure.Repositories;

namespace Todo.Business.Service;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll(ClaimsIdentity identity);
    TodoItem GetById(int id);
    bool Add(JsonElement body, ClaimsIdentity identity);
    bool Update(int id, JsonElement body);
    bool Delete(int id);
}

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    private static int getUserIdFromIdentity(ClaimsIdentity identity)
    {
        var userIdClaim = identity?.FindFirst("userId");

        if (userIdClaim == null)
            throw new Exception("Kullanıcı bulunamadı");

        string userId = userIdClaim.Value;

        return int.Parse(userId);
    }

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public IEnumerable<TodoItem> GetAll(ClaimsIdentity identity)
    {
        int userId = getUserIdFromIdentity(identity);

        return _todoRepository.GetAll(userId);
    }

    public TodoItem GetById(int id)
    {
        return _todoRepository.GetById(id);
    }

    public bool Add(JsonElement body, ClaimsIdentity identity)
    {
        var todoItem = JsonSerializer.Deserialize<TodoItem>(body.GetRawText());
        int userId = getUserIdFromIdentity(identity);

        if (todoItem == null)
            return false;

        todoItem.userId = userId;

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
