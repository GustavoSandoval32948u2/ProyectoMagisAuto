using FluentValidation;
using MagisAuto.Application.DTOs.NivelRiesgo;

namespace MagisAuto.Application.Validators.NivelRiesgo;

public class CrearNivelRiesgoValidator : AbstractValidator<CrearNivelRiesgoDto>
{
    public CrearNivelRiesgoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50);
    }
}