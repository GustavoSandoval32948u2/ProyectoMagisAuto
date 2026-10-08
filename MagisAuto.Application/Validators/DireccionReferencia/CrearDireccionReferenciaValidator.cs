using FluentValidation;
using MagisAuto.Application.DTOs.DireccionReferencia;

namespace MagisAuto.Application.Validators.DireccionReferencia;

public class CrearDireccionReferenciaValidator
    : AbstractValidator<CrearDireccionReferenciaDto>
{
    public CrearDireccionReferenciaValidator()
    {
        RuleFor(x => x.ReferenciaId)
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