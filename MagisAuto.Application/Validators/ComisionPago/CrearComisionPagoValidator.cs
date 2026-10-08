using FluentValidation;
using MagisAuto.Application.DTOs.ComisionPago;

namespace MagisAuto.Application.Validators.ComisionPago;

public class CrearComisionPagoValidator : AbstractValidator<CrearComisionPagoDto>
{
    public CrearComisionPagoValidator()
    {
        RuleFor(x => x.PagoId)
            .GreaterThan(0);

        RuleFor(x => x.MontoComision)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.FechaGeneracion)
            .LessThanOrEqualTo(DateTime.Now);
    }
}