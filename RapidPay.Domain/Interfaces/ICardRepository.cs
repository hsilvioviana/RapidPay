using RapidPay.Domain.Models;

namespace RapidPay.Domain.Interfaces
{
    public interface ICardRepository : IBaseRepository<CardModel>
    {
        Task<CardModel> GetByUserIdAsync(Guid userId);
    }
}
