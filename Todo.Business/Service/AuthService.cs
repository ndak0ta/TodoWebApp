using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Todo.Business.Models;
using Todo.Infrastructure.Repositories;


namespace Todo.Business.Service;

public interface IAuthService
{
    public string Login(LoginRequest loginRequest);
    public string GenerateToken(int userId);
    public string GetAuthToken();
    public bool IsTokenExpired(string token);
}

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public string Login(LoginRequest loginRequest)
    { 
        var userId = _userRepository.GetUserId(loginRequest.Username, loginRequest.Password);

        var token = GenerateToken(userId);

        return token;
    }

    public string GenerateToken(int userId)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var claims = new[]
        {
                new Claim("userId", userId.ToString()),
        };
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetAuthToken()
    {
        StringValues authorizationHeaders;
        _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationHeaders);
        var token = authorizationHeaders.ToString().Replace("Bearer ", string.Empty);
        return token;
    }

    public bool IsTokenExpired(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            return true;

        var now = DateTime.UtcNow;

        if (now > jwtToken.ValidTo)
            return true;

        return false;
    }
}


