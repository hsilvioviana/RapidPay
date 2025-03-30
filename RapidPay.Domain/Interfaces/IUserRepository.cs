using RapidPay.Domain.Models;

namespace RapidPay.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> SearchByUsernameAsync(string username);
        Task<UserModel> SearchByEmailAsync(string email);
    }
}
