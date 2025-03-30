namespace RapidPay.Services.Interfaces
{
    public interface IFeeService
    {
        Task<decimal> GetCurrentFee();
    }
}
