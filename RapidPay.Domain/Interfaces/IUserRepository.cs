using RapidPay.Domain.Models;

namespace RapidPay.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> SearchByUsername(string username);
        Task<UserModel> SearchByEmail(string email);
    }
}
