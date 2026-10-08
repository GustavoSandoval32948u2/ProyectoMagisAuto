using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Municipio;

namespace MagisAuto.Application.Validators.Municipio
{
    public class CrearMunicipioValidator
    : AbstractValidator<CrearMunicipioDto>
    {
        public CrearMunicipioValidator()
        {
            RuleFor(x => x.DepartamentoId)
                .GreaterThan(0)
                .WithMessage("El departamento es obligatorio.");

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre del municipio es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre del municipio no puede superar los 100 caracteres.");
        }
    }
}
