using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        var errorMessage = "Internal Server Error";

        switch (ex)
        {
            case UserNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorMessage = "Kullanıcı bulunamadı";
                break;
            case DuplicateRecordException:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                errorMessage = "Tekrarlı kayıt hatası";
                break;
            case DbUpdateException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorMessage = "Veritabanı hatası";
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var errorResponse = new { Message = errorMessage };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}

