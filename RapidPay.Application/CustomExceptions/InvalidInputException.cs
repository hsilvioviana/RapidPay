namespace RapidPay.Application.CustomExceptions
{
    public  class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {

        }
    }
}
