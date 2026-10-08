using System.Text.Json;
using MagisAutoAPI.Exceptions;
using Microsoft.Data.SqlClient;

namespace MagisAutoAPI.Middleware;

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
        catch (ApiException ex)
        {
            await HandleExceptionAsync(
                context,
                ex.StatusCode,
                ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status404NotFound,
                ex.Message);
        }
        catch (SqlException ex) when (
            ex.Number == 50011 ||
            ex.Number == 50012)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status404NotFound,
                ex.Message);
        }
        catch (SqlException ex) when (
            ex.Number >= 50010 &&
            ex.Number <= 50499)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "Ocurrió un error interno en el servidor.");
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        int statusCode,
        string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode,
            message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}