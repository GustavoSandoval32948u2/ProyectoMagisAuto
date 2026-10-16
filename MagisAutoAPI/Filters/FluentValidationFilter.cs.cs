
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagisAutoAPI.Filters;

public class FluentValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _services;

    public FluentValidationFilter(IServiceProvider services)
    {
        _services = services;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        foreach (var dto in context.ActionArguments.Values)
        {
            if (dto is null)
                continue;

            var tipoValidador = typeof(IValidator<>)
                .MakeGenericType(dto.GetType());

            var validador = _services.GetService(tipoValidador);

            if (validador is null)
                continue;

            var metodo = typeof(FluentValidationFilter)
                .GetMethod(nameof(ValidarAsync))!
                .MakeGenericMethod(dto.GetType());

            var tarea = (Task<List<FluentValidation.Results.ValidationFailure>>)
                metodo.Invoke(this, new[] { validador, dto })!;

            var errores = await tarea;

            foreach (var error in errores)
            {
                context.ModelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(
                new ValidationProblemDetails(context.ModelState));
            return;
        }

        await next();
    }

    public async Task<List<FluentValidation.Results.ValidationFailure>>
        ValidarAsync<T>(IValidator<T> validador, object dto)
    {
        var resultado = await validador.ValidateAsync((T)dto);
        return resultado.Errors;
    }
}
