using FluentValidation;
using RapidPay.Application.CustomExceptions;

namespace RapidPay.Application.Validations
{
    public static class ValidationHelper
    {
        public static void Validate<TV, TM>(TV validation, TM model) where TV : AbstractValidator<TM>
        {
            var result = validation.Validate(model);

            if (!result.IsValid)
            {
                throw new InvalidInputException(result.Errors[0].ToString());
            }
        }

        public static void ThrowErrorWhen<T, E>(T value1, string TypeOfComparison, T value2, E exception) where E : Exception
        {
            var throwError = false;

            if (TypeOfComparison == "Equal")
            {
                throwError = EqualityComparer<T>.Default.Equals(value1, value2);
            }
            else if (TypeOfComparison == "NotEqual")
            {
                throwError = !EqualityComparer<T>.Default.Equals(value1, value2);
            }

            if (throwError)
            {
                throw exception;
            }
        }
    }
}
