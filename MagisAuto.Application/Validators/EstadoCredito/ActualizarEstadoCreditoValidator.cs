using FluentValidation;
using MagisAuto.Application.DTOs.EstadoCredito;

namespace MagisAuto.Application.Validators.EstadoCredito;

public class ActualizarEstadoCreditoValidator : AbstractValidator<ActualizarEstadoCreditoDto>
{
    public ActualizarEstadoCreditoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50);
    }
}