using FluentValidation;
using MagisAuto.Application.DTOs.Concesionaria;

namespace MagisAuto.Application.Validators.Concesionaria;

public class CrearConcesionariaValidator
    : AbstractValidator<CrearConcesionariaDto>
{
    public CrearConcesionariaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.NIT)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.NIT));
    }
}