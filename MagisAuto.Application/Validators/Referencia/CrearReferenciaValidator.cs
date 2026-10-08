using FluentValidation;
using MagisAuto.Application.DTOs.Referencia;

namespace MagisAuto.Application.Validators.Referencia;

public class CrearReferenciaValidator
    : AbstractValidator<CrearReferenciaDto>
{
    public CrearReferenciaValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0);

        RuleFor(x => x.ParentescoId)
            .GreaterThan(0);

        RuleFor(x => x.LugarTrabajo)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.NombreCompleto)
            .NotEmpty()
            .MaximumLength(150);
    }
}