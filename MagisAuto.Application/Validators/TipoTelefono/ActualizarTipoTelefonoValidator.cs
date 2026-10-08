using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.TipoTelefono;

namespace MagisAuto.Application.Validators.TipoTelefono
{
    public class ActualizarTipoTelefonoValidator
    : AbstractValidator<ActualizarTipoTelefonoDto>
    {
        public ActualizarTipoTelefonoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(30)
                .WithMessage("El nombre no puede superar los 30 caracteres.");
        }
    }
}
