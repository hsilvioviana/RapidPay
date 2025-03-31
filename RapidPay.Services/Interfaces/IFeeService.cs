namespace RapidPay.Services.Interfaces
{
    public interface IFeeService
    {
        decimal GetCurrentFee();
        Task UpdateFeeAsync();
    }
}
