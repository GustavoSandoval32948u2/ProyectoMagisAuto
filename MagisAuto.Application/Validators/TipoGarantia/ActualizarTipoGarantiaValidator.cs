using FluentValidation;
using MagisAuto.Application.DTOs.TipoGarantia;

namespace MagisAuto.Application.Validators.TipoGarantia;

public class ActualizarTipoGarantiaValidator : AbstractValidator<ActualizarTipoGarantiaDto>
{
    public ActualizarTipoGarantiaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(x => x.Descripcion)
            .NotEmpty()
            .MaximumLength(250);
    }
}