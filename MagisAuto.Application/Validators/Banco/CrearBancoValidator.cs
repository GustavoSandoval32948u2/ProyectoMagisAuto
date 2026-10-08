using FluentValidation;
using MagisAuto.Application.DTOs.Banco;

namespace MagisAuto.Application.Validators.Banco;

public class CrearBancoValidator : AbstractValidator<CrearBancoDto>
{
    public CrearBancoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(120);
    }
}