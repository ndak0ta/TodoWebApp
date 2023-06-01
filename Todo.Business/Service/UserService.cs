using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Todo.Infrastructure.Repositories;
using Todo.Data.Models;
using Todo.Business.Models;


namespace Todo.Business.Service;

public interface IUserService
{
    public int GetUserId(User user);
    public bool Add(User user);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public int GetUserId(User user)
    {
        return _userRepository.GetUserId(user);
    }

    public bool Add(User user)
    {
        return _userRepository.Add(user);
    }
}

