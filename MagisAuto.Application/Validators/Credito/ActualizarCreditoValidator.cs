using FluentValidation;
using MagisAuto.Application.DTOs.Credito;

namespace MagisAuto.Application.Validators.Credito;

public class ActualizarCreditoValidator
    : AbstractValidator<ActualizarCreditoDto>
{
    public ActualizarCreditoValidator()
    {
        RuleFor(x => x.EstadoCreditoId)
            .GreaterThan(0);

        RuleFor(x => x.NivelRiesgoId)
            .GreaterThan(0);

        RuleFor(x => x.PorcentajeComision)
            .InclusiveBetween(0, 100);
    }
}