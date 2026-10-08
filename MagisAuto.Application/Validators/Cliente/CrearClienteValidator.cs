using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Cliente;

namespace MagisAuto.Application.Validators.Cliente
{

    public class CrearClienteValidator : AbstractValidator<CrearClienteDto>
    {
        public CrearClienteValidator()
        {
            RuleFor(x => x.Nit)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Dpi)
                .NotEmpty()
                .Length(13)
                .Matches(@"^\d+$");

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
