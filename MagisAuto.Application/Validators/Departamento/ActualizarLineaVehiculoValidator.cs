using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Departamento;

namespace MagisAuto.Application.Validators.LineaVehiculo
{
    public class ActualizarDepartamentoValidator : AbstractValidator<ActualizarDepartamentoDto>
    {
        public ActualizarDepartamentoValidator()
        {
            RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre del departamento es obligatorio.")
            .MaximumLength(100)
            .WithMessage("El nombre del departamento no puede superar los 100 caracteres.");
        }
    }
}
