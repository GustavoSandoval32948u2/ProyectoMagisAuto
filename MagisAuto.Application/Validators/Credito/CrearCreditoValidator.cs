using FluentValidation;
using MagisAuto.Application.DTOs.Credito;

namespace MagisAuto.Application.Validators.Credito;

public class CrearCreditoValidator
    : AbstractValidator<CrearCreditoDto>
{
    public CrearCreditoValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0);

        RuleFor(x => x.VehiculoId)
            .GreaterThan(0);

        RuleFor(x => x.VendedorId)
            .GreaterThan(0);

        RuleFor(x => x.NivelRiesgoId)
            .GreaterThan(0);

        RuleFor(x => x.MontoEnganche)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PorcentajeComision)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.PlazoMeses)
            .GreaterThan(0)
            .When(x => x.PlazoMeses.HasValue);

        RuleFor(x => x.TasaInteresAnual)
            .InclusiveBetween(0, 100)
            .When(x => x.TasaInteresAnual.HasValue);

        RuleFor(x => x.FechaAprobacion)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.FechaAprobacion.HasValue);
    }
}