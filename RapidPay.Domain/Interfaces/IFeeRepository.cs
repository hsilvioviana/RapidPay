using RapidPay.Domain.Models;

namespace RapidPay.Domain.Interfaces
{
    public interface IFeeRepository : IBaseRepository<FeeModel>
    {
        Task<FeeModel> GetLastAsync();
    }
}
