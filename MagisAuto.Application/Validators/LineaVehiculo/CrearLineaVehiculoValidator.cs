using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.LineaVehiculo;

namespace MagisAuto.Application.Validators.LineaVehiculo
{
    public class CrearLineaVehiculoValidator : AbstractValidator<CrearLineaVehiculoDto>
    {
        public CrearLineaVehiculoValidator()
        {
            RuleFor(x => x.MarcaId)
                .GreaterThan(0)
                .WithMessage("La marca es obligatoria.");

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede superar los 100 caracteres.");
        }
    }
}
