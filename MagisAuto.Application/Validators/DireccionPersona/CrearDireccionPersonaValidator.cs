using FluentValidation;
using MagisAuto.Application.DTOs.DireccionPersona;

namespace MagisAuto.Application.Validators.DireccionPersona;

public class CrearDireccionPersonaValidator
    : AbstractValidator<CrearDireccionPersonaDto>
{
    public CrearDireccionPersonaValidator()
    {
        RuleFor(x => x.PersonaId)
            .GreaterThan(0);

        RuleFor(x => x.MunicipioId)
            .GreaterThan(0);

        RuleFor(x => x.Zona)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.Zona));

        RuleFor(x => x.Direccion)
            .NotEmpty()
            .MaximumLength(250);
    }
}