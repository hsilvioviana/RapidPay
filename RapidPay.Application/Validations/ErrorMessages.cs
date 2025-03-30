namespace RapidPay.Application.Validations
{
    public static class ErrorMessages
    {
        public static class Authentication
        {
            public const string UsernameAlreadyInUse = "The provided username is already in use.";
            public const string EmailAlreadyInUse = "The provided email is already in use.";
            public const string UserNotFound = "User not found.";
            public const string IncorrectPassword = "Incorrect password.";
        }
    }
}
