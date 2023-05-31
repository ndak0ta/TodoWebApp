using Microsoft.EntityFrameworkCore;
using Todo.Data.Contexts;
using Todo.Data.Models;


namespace Todo.Infrastructure.Repositories;

public interface IUserRepository
{
    public int GetUserId(string? username, string? password);
    public bool Add(User user);
    // public bool Update(int id, User user);
    // public bool Delete(int id);
}

public class UserRepository: IUserRepository
{
    private readonly TodoDbContext _todoDbContext;

	public UserRepository(TodoDbContext todoDbContext)
	{
        _todoDbContext = todoDbContext;
	}
    
    public int GetUserId(string? username, string? password)
    {
        var result = _todoDbContext.Set<User>().First(u => u.userName == username && u.password == password);

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

    /*

    public bool Update(int id, User user)
    {

    }

    public bool Delete(int id)
    {

    }

    */
}


