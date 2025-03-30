using FluentValidation;
using RapidPay.Application.ViewModels;

namespace RapidPay.Application.Validations
{
    public class CardPaymentValidation : AbstractValidator<CardPaymentViewModel>
    {
        public CardPaymentValidation()
        {
            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than 0.");
        }
    }
}