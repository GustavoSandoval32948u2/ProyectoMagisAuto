using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MagisAuto.Application.DTOs.Parentesco;

namespace MagisAuto.Application.Validators.Parentesco
{
    public class CrearParentescoValidator : AbstractValidator<CrearParentescoDto>
    {
        public CrearParentescoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(80);
        }
    }
}
