using FluentValidation;
using MagisAuto.Application.DTOs.TelefonoPersona;

namespace MagisAuto.Application.Validators.TelefonoPersona;

public class CrearTelefonoPersonaValidator
    : AbstractValidator<CrearTelefonoPersonaDto>
{
    public CrearTelefonoPersonaValidator()
    {
        RuleFor(x => x.PersonaId)
            .GreaterThan(0);

        RuleFor(x => x.TipoTelefonoId)
            .GreaterThan(0);

        RuleFor(x => x.NumeroTelefono)
            .NotEmpty()
            .MaximumLength(20);
    }
}