using FluentValidation;
using MagisAuto.Application.DTOs.Vendedor;

namespace MagisAuto.Application.Validators.Vendedor;

public class ActualizarVendedorValidator
    : AbstractValidator<ActualizarVendedorDto>
{
    public ActualizarVendedorValidator()
    {
        RuleFor(x => x.CodigoEmpleado)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.DPI)
            .NotEmpty()
            .Length(13)
            .Matches(@"^\d{13}$");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.AgenciaId)
            .GreaterThan(0);
    }
}