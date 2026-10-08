using FluentValidation;
using MagisAuto.Application.DTOs.Pago;

namespace MagisAuto.Application.Validators.Pago;

public class ActualizarPagoValidator : AbstractValidator<ActualizarPagoDto>
{
    public ActualizarPagoValidator()
    {
        RuleFor(x => x.CuotaId)
            .GreaterThan(0);

        RuleFor(x => x.NumeroComprobante)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.MontoPagado)
            .GreaterThan(0);

        RuleFor(x => x.MedioPagoId)
            .GreaterThan(0);

        RuleFor(x => x.BancoId)
            .GreaterThan(0)
            .When(x => x.BancoId.HasValue);

        RuleFor(x => x.ReferenciaPago)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.ReferenciaPago));

        RuleFor(x => x.FechaHoraPago)
            .LessThanOrEqualTo(DateTime.Now)
            .When(x => x.FechaHoraPago.HasValue);
    }
}