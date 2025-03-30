namespace RapidPay.Services.Interfaces
{
    public interface ILogService
    {
        Task TrackChange(Guid cardId, string field, string from, string to);
    }
}
