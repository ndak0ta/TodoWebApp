using Microsoft.EntityFrameworkCore;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Infrastructure.Exceptions;


namespace Todo.Infrastructure.Repositories;

public interface IUserRepository
{
    public User GetUserByUsername(string username);
    public int GetUserId(User user);
    public void Add(User user);
    public void Delete(int userId);
}

public class UserRepository: IUserRepository
{
    private readonly TodoDbContext _todoDbContext;

	public UserRepository(TodoDbContext todoDbContext)
	{
        _todoDbContext = todoDbContext;
	}

    public User GetUserByUsername(string? username)
    {
        var result = _todoDbContext.Set<User>().FirstOrDefault(u => u.Username == username);

        if (result == null)
            throw new NotFoundException("Kullanıcı bulunamadı.");

        return result;
    }

    public int GetUserId(User inputUser)
    {
        var user = _todoDbContext.Set<User>().FirstOrDefault(u => u.Username == inputUser.Username && u.Password == inputUser.Password);

        if (user == null)
            throw new NotFoundException("Kullanıcı bulunamadı.");

        return user.Id;
    }

    public void Add(User user)
    {
        var existingUser = GetUserByUsername(user.Username);

        if (existingUser != null)
            throw new DuplicateRecordException("Aynı kullanıcı adıyla başka bir kullanıcı zaten mevcut.");

        _todoDbContext.Set<User>().Add(user);
        _todoDbContext.SaveChanges();
    }

    public void Delete(int userId)
    {
        var user = _todoDbContext.Set<User>().Find(userId);

        if (user == null)
            throw new NotFoundException("Kullanıcı bulunamadı.");

        _todoDbContext.Set<User>().Remove(user);
        _todoDbContext.SaveChanges();
    }
}


