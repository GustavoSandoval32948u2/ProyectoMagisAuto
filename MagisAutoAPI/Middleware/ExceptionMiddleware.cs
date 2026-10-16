using System.Text.Json;
using MagisAutoAPI.Exceptions;
using Microsoft.Data.SqlClient;

namespace MagisAutoAPI.Middleware;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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
                context, ex.StatusCode, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(
                context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (SqlException ex) when (
            ex.Number == 50011 || ex.Number == 50012)
        {
            await HandleExceptionAsync(
                context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (SqlException ex) when (
            ex.Number == 50001 ||
            ex.Number == 50002 ||
            ex.Number == 50003 ||
            ex.Number == 50004)
        {
            await HandleExceptionAsync(
                context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (SqlException ex) when (
            ex.Number >= 50010 && ex.Number <= 50499)
        {
            await HandleExceptionAsync(
                context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            _logger.LogWarning(
                ex,
                "Error de integridad referencial en {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await HandleExceptionAsync(
                context,
                StatusCodes.Status400BadRequest,
                "No se pudo completar la operación porque existe una referencia relacionada inválida. Verifica los datos relacionados e inténtalo nuevamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error inesperado en {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

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
