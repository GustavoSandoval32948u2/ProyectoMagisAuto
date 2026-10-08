using FluentValidation;
using MagisAuto.Application.DTOs.Cuota;

namespace MagisAuto.Application.Validators.Cuota;

public class ActualizarCuotaValidator : AbstractValidator<ActualizarCuotaDto>
{
    public ActualizarCuotaValidator()
    {
        RuleFor(x => x.FechaVencimiento)
            .NotEmpty();
    }
}