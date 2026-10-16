using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Cliente;
using MagisAuto.Application.Validators.Validaciones;

namespace MagisAuto.Application.Validators.Cliente
{

    public class CrearClienteValidator : AbstractValidator<CrearClienteDto>
    {
        public CrearClienteValidator()
        {

            RuleFor(x => x.Nit)
                .NotEmpty()
                .WithMessage("El NIT es obligatorio.")
                .Must(ValidadorNitGuatemala.EsValido)
                .WithMessage("El NIT no tiene un formato o dígito verificador válido.");

            RuleFor(x => x.Dpi)
                .NotEmpty()
                .WithMessage("El DPI es obligatorio.")
                .Length(13)
                .WithMessage("El DPI debe tener exactamente 13 dígitos.")
                .Matches(@"^\d+$")
                .WithMessage("El DPI solo debe contener números.");

            RuleFor(x => x.NombreCompleto)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.FechaNacimiento)
                .LessThanOrEqualTo(DateTime.Today)
                .When(x => x.FechaNacimiento.HasValue);
        }
    }
}
