using System.Net;
using System.Text.Json;
using FluentValidation;
using ViajaBrasil.Domain.Exceptions;

namespace ViajaBrasil.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(x => new
            {
                Field = x.PropertyName,
                Message = x.ErrorMessage
            });
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, errors);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                $"Internal server error: {ex.Message}"
            );
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, object errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            Errors = errors
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}