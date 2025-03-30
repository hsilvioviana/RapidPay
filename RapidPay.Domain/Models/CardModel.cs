namespace RapidPay.Domain.Models
{
    public class CardModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool Active { get; set; }
    }
}
