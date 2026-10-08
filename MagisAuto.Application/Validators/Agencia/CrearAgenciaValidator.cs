using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Agencia;

namespace MagisAuto.Application.Validators.Agencia
{
    public class CrearAgenciaValidator : AbstractValidator<CrearAgenciaDto>
    {
        public CrearAgenciaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.RegionId)
                .GreaterThan(0)
                .WithMessage("La región es obligatoria.");

            RuleFor(x => x.MunicipioId)
                .GreaterThan(0)
                .WithMessage("El municipio es obligatorio.");

            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage("La dirección es obligatoria.")
                .MaximumLength(250)
                .WithMessage("La dirección no puede superar los 250 caracteres.");
        }
    }
}
