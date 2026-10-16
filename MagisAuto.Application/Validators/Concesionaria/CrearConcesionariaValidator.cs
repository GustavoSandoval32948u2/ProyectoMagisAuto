using FluentValidation;
using MagisAuto.Application.DTOs.Concesionaria;
using MagisAuto.Application.Validators.Validaciones;

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
            .NotEmpty()
            .WithMessage("El NIT es obligatorio.")
            .Must(ValidadorNitGuatemala.EsValido)
            .WithMessage("El NIT no tiene un formato o dígito verificador válido.");
    }
}