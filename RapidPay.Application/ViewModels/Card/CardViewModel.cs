namespace RapidPay.Application.ViewModels
{
    public class CardViewModel
    {
        public string Number { get; set; }
        public string CVV { get; set; }
        public string ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool Active { get; set; }
    }
}
