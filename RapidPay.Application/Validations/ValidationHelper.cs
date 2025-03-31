using FluentValidation;
using RapidPay.Application.CustomExceptions;

namespace RapidPay.Application.Validations
{
    public static class ValidationHelper
    {
        public enum ComparisonType
        {
            Equal,
            NotEqual
        }

        public static void Validate<TV, TM>(TV validation, TM model) where TV : AbstractValidator<TM>
        {
            var result = validation.Validate(model);

            if (!result.IsValid)
            {
                throw new InvalidInputException(result.Errors[0].ToString());
            }
        }

        public static void ThrowErrorWhen<T, E>(T value1, ComparisonType comparison, T value2, E exception) where E : Exception
        {
            bool throwError = comparison switch
            {
                ComparisonType.Equal => Equals(value1, value2),
                ComparisonType.NotEqual => !Equals(value1, value2),
                _ => false
            };

            if (throwError)
            {
                throw exception;
            }
        }
    }
}
