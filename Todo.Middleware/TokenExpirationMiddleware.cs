using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Todo.Business.Service;
using Todo.Data.Contexts;


namespace Todo.Middleware;

public class TokenExpirationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public TokenExpirationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

            var token = authService.GetAuthToken();

            if (!string.IsNullOrEmpty(token) && authService.IsTokenExpired(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}

