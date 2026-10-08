using FluentValidation;
using MagisAuto.Application.DTOs.TelefonoReferencia;

namespace MagisAuto.Application.Validators.TelefonoReferencia;

public class CrearTelefonoReferenciaValidator
    : AbstractValidator<CrearTelefonoReferenciaDto>
{
    public CrearTelefonoReferenciaValidator()
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