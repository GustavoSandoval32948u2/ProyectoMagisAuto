using FluentValidation;
using MagisAuto.Application.DTOs.ComisionPago;

namespace MagisAuto.Application.Validators.ComisionPago;

public class ActualizarComisionPagoValidator : AbstractValidator<ActualizarComisionPagoDto>
{
    public ActualizarComisionPagoValidator()
    {
        RuleFor(x => x.PagoId)
            .GreaterThan(0);

        RuleFor(x => x.MontoComision)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.FechaGeneracion)
            .LessThanOrEqualTo(DateTime.Now);
    }
}