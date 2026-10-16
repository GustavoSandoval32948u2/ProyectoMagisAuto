using FluentValidation;
using MagisAuto.Application.DTOs.Persona;

namespace MagisAuto.Application.Validators.Persona;

public class CrearPersonaValidator : AbstractValidator<CrearPersonaDto>
{
    public CrearPersonaValidator()
    {
        RuleFor(x => x.NIT)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.NIT));

        RuleFor(x => x.NIT)
            .Matches(@"^[0-9]{1,12}-?[0-9Kk]$")
            .WithMessage("El NIT debe tener un formato válido.")
            .When(x => !string.IsNullOrWhiteSpace(x.NIT));

        RuleFor(x => x.DPI)
            .NotEmpty()
            .Length(13)
            .Matches(@"^\d{13}$");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.CorreoElectronico)
            .NotEmpty()
            .MaximumLength(150)
            .EmailAddress();

        RuleFor(x => x.FechaNacimiento)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.FechaNacimiento.HasValue);
    }
}