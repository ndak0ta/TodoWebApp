using Microsoft.EntityFrameworkCore;

namespace Todo.Data.Repositories;

public class TodoRepository
{
    private readonly DbContext _dbContext;

    public TodoRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Todo> GetAll()
    {
        return _dbContext.Set<Todo>().ToList();
    }
}