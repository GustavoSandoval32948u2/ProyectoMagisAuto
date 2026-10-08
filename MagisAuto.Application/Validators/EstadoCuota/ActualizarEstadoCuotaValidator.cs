using FluentValidation;
using MagisAuto.Application.DTOs.EstadoCuota;

namespace MagisAuto.Application.Validators.EstadoCuota;

public class ActualizarEstadoCuotaValidator : AbstractValidator<ActualizarEstadoCuotaDto>
{
    public ActualizarEstadoCuotaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(80);
    }
}