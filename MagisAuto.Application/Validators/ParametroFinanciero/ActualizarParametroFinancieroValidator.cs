using FluentValidation;
using MagisAuto.Application.DTOs.ParametroFinanciero;

namespace MagisAuto.Application.Validators.ParametroFinanciero;

public class ActualizarParametroFinancieroValidator
    : AbstractValidator<ActualizarParametroFinancieroDto>
{
    public ActualizarParametroFinancieroValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(x => x.Valor)
            .GreaterThanOrEqualTo(0);
    }
}