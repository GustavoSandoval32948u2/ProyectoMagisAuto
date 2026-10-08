using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Marca;

namespace MagisAuto.Application.Validators.Marca
{
    public class ActualizarMarcaValidator
    : AbstractValidator<ActualizarMarcaDto>
    {
        public ActualizarMarcaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(x => x.CodigoInterno)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
