using FluentValidation;
using MagisAuto.Application.DTOs.NivelRiesgo;

namespace MagisAuto.Application.Validators.NivelRiesgo;

public class ActualizarNivelRiesgoValidator : AbstractValidator<ActualizarNivelRiesgoDto>
{
    public ActualizarNivelRiesgoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50);
    }
}