using FluentValidation;
using MagisAuto.Application.DTOs.Banco;

namespace MagisAuto.Application.Validators.Banco;

public class ActualizarBancoValidator : AbstractValidator<ActualizarBancoDto>
{
    public ActualizarBancoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(120);
    }
}