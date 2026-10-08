using FluentValidation;
using MagisAuto.Application.DTOs.Persona;

namespace MagisAuto.Application.Validators.Persona;

public class CrearPersonaValidator : AbstractValidator<CrearPersonaDto>
{
    public CrearPersonaValidator()
    {
        RuleFor(x => x.NIT)
            .NotEmpty()
            .MaximumLength(20);

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