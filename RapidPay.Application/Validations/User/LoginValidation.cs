using FluentValidation;
using RapidPay.Application.ViewModels;

namespace RapidPay.Application.Validations
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        public LoginValidation()
        {
            RuleFor(t => t.Username)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters long.")
                .MaximumLength(30).WithMessage("{PropertyName} must be at most 30 characters long.");

            RuleFor(t => t.Password)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .MinimumLength(6).WithMessage("{PropertyName} must be at least 6 characters long.")
                .MaximumLength(30).WithMessage("{PropertyName} must be at most 30 characters long.");
        }
    }
}
