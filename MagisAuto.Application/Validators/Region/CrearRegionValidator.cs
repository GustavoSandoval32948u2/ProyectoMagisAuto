using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Region;

namespace MagisAuto.Application.Validators.Region
{
    public class CrearRegionValidator : AbstractValidator<CrearRegionDto>
    {
        public CrearRegionValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la region es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre de la region no puede superar los 100 caracteres.");
        }
    }
}
