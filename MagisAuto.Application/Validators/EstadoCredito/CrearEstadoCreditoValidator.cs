using FluentValidation;
using MagisAuto.Application.DTOs.EstadoCredito;

namespace MagisAuto.Application.Validators.EstadoCredito;

public class CrearEstadoCreditoValidator : AbstractValidator<CrearEstadoCreditoDto>
{
    public CrearEstadoCreditoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50);
    }
}