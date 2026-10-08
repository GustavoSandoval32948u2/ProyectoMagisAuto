using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Vehiculo;

namespace MagisAuto.Application.Validators.Vehiculo
{
    public class ActualizarVehiculoValidator : AbstractValidator<ActualizarVehiculoDto>
    {
        public ActualizarVehiculoValidator()
        {
            RuleFor(x => x.LineaVehiculoId)
                .GreaterThan(0)
                .WithMessage("La línea del vehículo es obligatoria.");

            RuleFor(x => x.ConcesionariaId)
                .GreaterThan(0)
                .WithMessage("La concesionaria es obligatoria.");

            RuleFor(x => x.Vin)
                .NotEmpty()
                .WithMessage("El VIN es obligatorio.")
                .Length(17)
                .WithMessage("El VIN debe tener exactamente 17 caracteres.");

            RuleFor(x => x.AnioFabricacion)
                .InclusiveBetween((short)2000, (short)2100)
                .WithMessage("El año de fabricación debe estar entre 2000 y 2100.");

            RuleFor(x => x.ValorAgencia)
                .GreaterThan(0)
                .WithMessage("El valor de agencia debe ser mayor que 0.");

            RuleFor(x => x.Placa)
                .MaximumLength(15)
                .When(x => !string.IsNullOrWhiteSpace(x.Placa))
                .WithMessage("La placa no puede superar los 15 caracteres.");
        }
    }
}
