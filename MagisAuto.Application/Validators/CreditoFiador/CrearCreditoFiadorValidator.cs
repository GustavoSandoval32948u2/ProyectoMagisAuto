using FluentValidation;
using MagisAuto.Application.DTOs.CreditoFiador;

namespace MagisAuto.Application.Validators.CreditoFiador;

public class CrearCreditoFiadorValidator : AbstractValidator<CrearCreditoFiadorDto>
{
    public CrearCreditoFiadorValidator()
    {
        RuleFor(x => x.CreditoId)
            .GreaterThan(0);

        RuleFor(x => x.PersonaId)
            .GreaterThan(0);
    }
}