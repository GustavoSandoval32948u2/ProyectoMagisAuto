using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Parentesco;

namespace MagisAuto.Application.Validators.Parentesco
{
    public class ActualizarParentescoValidator : AbstractValidator<ActualizarParentescoDto>
    {
        public ActualizarParentescoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(80);
        }
    }
}
