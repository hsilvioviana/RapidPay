namespace RapidPay.Application.CustomExceptions
{
    public  class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException(string message) : base(message)
        {

        }
    }
}
