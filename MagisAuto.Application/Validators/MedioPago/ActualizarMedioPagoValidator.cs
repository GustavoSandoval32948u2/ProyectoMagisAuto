using FluentValidation;
using MagisAuto.Application.DTOs.MedioPago;

namespace MagisAuto.Application.Validators.MedioPago;

public class ActualizarMedioPagoValidator : AbstractValidator<ActualizarMedioPagoDto>
{
    public ActualizarMedioPagoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50);
    }
}