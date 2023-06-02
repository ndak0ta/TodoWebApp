using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Todo.Infrastructure.Repositories;
using Todo.Infrastructure.Exceptions;
using Todo.Data.Models;
using Todo.Business.Models;


namespace Todo.Business.Service;

public interface IUserService
{
    public int GetUserId(User user);
    public void Add(User user);
    public void Delete(string userId);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITodoService _todoService;

    public UserService(IUserRepository userRepository, ITodoService todoService)
    {
        _userRepository = userRepository;
        _todoService = todoService;
    }

    public int GetUserId(User user)
    {

        return _userRepository.GetUserId(user);

    }

    public void Add(User user)
    {
        _userRepository.Add(user);
    }

    public void Delete(string userId)
    {
        _userRepository.Delete(int.Parse(userId));
        _todoService.DeleteAllByUserId(userId);
    }
}

