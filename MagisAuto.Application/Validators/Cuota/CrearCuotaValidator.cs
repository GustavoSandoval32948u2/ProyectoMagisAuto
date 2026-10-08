using FluentValidation;
using MagisAuto.Application.DTOs.Cuota;

namespace MagisAuto.Application.Validators.Cuota;

public class CrearCuotaValidator : AbstractValidator<CrearCuotaDto>
{
    public CrearCuotaValidator()
    {
        RuleFor(x => x.CreditoId)
            .GreaterThan(0);

        RuleFor(x => x.NumeroCuota)
            .GreaterThan(0);

        RuleFor(x => x.MontoCapital)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MontoInteres)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MontoManejoCuenta)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MontoSeguroVehiculo)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MontoSeguroVida)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.EstadoCuotaId)
            .GreaterThan(0);
    }
}