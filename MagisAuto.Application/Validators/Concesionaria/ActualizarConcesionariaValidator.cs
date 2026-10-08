using FluentValidation;
using MagisAuto.Application.DTOs.Concesionaria;

namespace MagisAuto.Application.Validators.Concesionaria;

public class ActualizarConcesionariaValidator
    : AbstractValidator<ActualizarConcesionariaDto>
{
    public ActualizarConcesionariaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.NIT)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.NIT));
    }
}