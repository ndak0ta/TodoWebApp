using Microsoft.EntityFrameworkCore;
using Todo.Data.Contexts;
using Todo.Data.Models;


namespace Todo.Infrastructure.Repositories;

public interface IUserRepository
{
    public int GetUserId(User user);
    public bool Add(User user);
    public bool Delete(int userId);
}

public class UserRepository: IUserRepository
{
    private readonly TodoDbContext _todoDbContext;

	public UserRepository(TodoDbContext todoDbContext)
	{
        _todoDbContext = todoDbContext;
	}
    
    public int GetUserId(User user)
    {
        var result = _todoDbContext.Set<User>().First(u => u.Username == user.Username && u.Password == user.Password);

        if (result == null)
            throw new Exception("kullanıcı bulunamadı");

        return result.Id;
    }

    public bool Add(User user)
    {
        try
        {
            _todoDbContext.Set<User>().Add(user);
            _todoDbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(int userId)
    {
        var user = _todoDbContext.Set<User>().Find(userId);

        if (user == null)
            return false;

        _todoDbContext.Set<User>().Remove(user);
        _todoDbContext.SaveChanges();

        return true;
    }
}


