using FluentValidation;
using RapidPay.Application.ViewModels;

namespace RapidPay.Application.Validations
{
    public class CardUpdateValidation : AbstractValidator<CardUpdateViewModel>
    {
        public CardUpdateValidation()
        {
            RuleFor(c => c.Balance)
                .GreaterThanOrEqualTo(0).When(c => c.Balance.HasValue)
                .WithMessage("Balance must be 0 or greater.");

            RuleFor(c => c.CreditLimit)
                .GreaterThanOrEqualTo(0).When(c => c.CreditLimit.HasValue)
                .WithMessage("CreditLimit must be 0 or greater.");
        }
    }
}

