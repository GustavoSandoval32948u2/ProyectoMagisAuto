using FluentValidation;
using MagisAuto.Application.DTOs.TelefonoReferencia;

namespace MagisAuto.Application.Validators.TelefonoReferencia;

public class ActualizarTelefonoReferenciaValidator
    : AbstractValidator<ActualizarTelefonoReferenciaDto>
{
    public ActualizarTelefonoReferenciaValidator()
    {
        RuleFor(x => x.ReferenciaId)
            .GreaterThan(0);

        RuleFor(x => x.TipoTelefonoId)
            .GreaterThan(0);

        RuleFor(x => x.NumeroTelefono)
            .NotEmpty()
            .MaximumLength(20);
    }
}