using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Todo.Data.Models;


namespace Todo.Business.Service;

public interface IAuthService
{
    public Task<string> LoginAsync(User user);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthService(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<string> LoginAsync(User user)
    {
        var userId = await _userService.GetUserIdAsync(user);

        var accesstoken = GenerateAccessToken(userId);

        return accesstoken;
    }

    private string GenerateAccessToken(int userId)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var claims = new[]
        {
            new Claim("userId", userId.ToString(), ClaimValueTypes.Integer),
        };
        var expires = DateTime.UtcNow.AddMinutes(1);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


