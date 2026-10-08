using FluentValidation;
using MagisAuto.Application.DTOs.HistorialDepreciacion;

namespace MagisAuto.Application.Validators.HistorialDepreciacion;

public class CrearHistorialDepreciacionValidator
    : AbstractValidator<CrearHistorialDepreciacionDto>
{
    public CrearHistorialDepreciacionValidator()
    {
        RuleFor(x => x.VehiculoId)
            .GreaterThan(0);

        RuleFor(x => x.FechaRegistro)
            .NotEmpty();

        RuleFor(x => x.PorcentajeDepreciacion)
            .InclusiveBetween(0, 100);
    }
}