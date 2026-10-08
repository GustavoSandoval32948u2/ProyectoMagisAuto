using FluentValidation;
using MagisAuto.Application.DTOs.TelefonoPersona;

namespace MagisAuto.Application.Validators.TelefonoPersona;

public class ActualizarTelefonoPersonaValidator
    : AbstractValidator<ActualizarTelefonoPersonaDto>
{
    public ActualizarTelefonoPersonaValidator()
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