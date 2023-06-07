using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;


namespace Todo.Middleware;

public class TokenExpirationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenExpirationMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
        _serviceProvider = serviceProvider;
    }

    private string GetAuthToken()
    {
        StringValues authorizationHeaders;
        _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationHeaders);
        var token = authorizationHeaders.ToString().Replace("Bearer ", string.Empty);
        return token;
    }

    private static bool IsTokenExpired(string token)
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

    public async Task Invoke(HttpContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var token = GetAuthToken();

            if (!string.IsNullOrEmpty(token) && IsTokenExpired(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}

