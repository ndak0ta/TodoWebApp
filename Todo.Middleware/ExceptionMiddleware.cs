using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.Infrastructure.Exceptions;

namespace Todo.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorMessage = "Internal Server Error";

        if (ex is UserNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            errorMessage = "Kullanıcı bulunamadı";
        }

        var errorResponse = new { Message = errorMessage };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}

