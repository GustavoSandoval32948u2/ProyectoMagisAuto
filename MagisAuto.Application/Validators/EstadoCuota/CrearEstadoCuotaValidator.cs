using FluentValidation;
using MagisAuto.Application.DTOs.EstadoCuota;

namespace MagisAuto.Application.Validators.EstadoCuota;

public class CrearEstadoCuotaValidator : AbstractValidator<CrearEstadoCuotaDto>
{
    public CrearEstadoCuotaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(80);
    }
}