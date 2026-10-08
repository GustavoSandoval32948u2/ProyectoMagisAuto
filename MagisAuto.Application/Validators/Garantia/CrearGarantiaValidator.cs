using FluentValidation;
using MagisAuto.Application.DTOs.Garantia;

namespace MagisAuto.Application.Validators.Garantia;

public class CrearGarantiaValidator : AbstractValidator<CrearGarantiaDto>
{
    public CrearGarantiaValidator()
    {
        RuleFor(x => x.CreditoId)
            .GreaterThan(0);

        RuleFor(x => x.TipoGarantiaId)
            .GreaterThan(0);

        RuleFor(x => x.Descripcion)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.ValorGarantia)
            .GreaterThanOrEqualTo(0);
    }
}