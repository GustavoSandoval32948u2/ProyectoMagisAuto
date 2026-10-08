using FluentValidation;
using MagisAuto.Application.DTOs.MedioPago;

namespace MagisAuto.Application.Validators.MedioPago;

public class CrearMedioPagoValidator : AbstractValidator<CrearMedioPagoDto>
{
    public CrearMedioPagoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50);
    }
}