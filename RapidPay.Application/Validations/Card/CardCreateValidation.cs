using FluentValidation;
using RapidPay.Application.ViewModels;

namespace RapidPay.Application.Validations
{
    public class CardCreateValidation : AbstractValidator<CardCreateViewModel>
    {
        public CardCreateValidation()
        {
            RuleFor(c => c.CreditLimit)
                .GreaterThanOrEqualTo(0).When(c => c.CreditLimit.HasValue)
                .WithMessage("CreditLimit must be 0 or greater.");
        }
    }
}
