using FluentValidation;
using RapidPay.Application.ViewModels;

namespace RapidPay.Application.Validations
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserViewModel>
    {
        public UpdateUserValidation()
        {
            RuleFor(t => t.Username)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters long.")
                .MaximumLength(30).WithMessage("{PropertyName} must be at most 30 characters long.");

            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(t => t.Password)
                .MinimumLength(6).WithMessage("{PropertyName} must be at least 6 characters long.")
                .MaximumLength(30).WithMessage("{PropertyName} must be at most 30 characters long.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(t => t.NewPassword)
                .MinimumLength(6).WithMessage("{PropertyName} must be at least 6 characters long.")
                .MaximumLength(30).WithMessage("{PropertyName} must be at most 30 characters long.")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));
        }
    }
}
