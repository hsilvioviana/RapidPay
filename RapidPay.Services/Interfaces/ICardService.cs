using RapidPay.Application.ViewModels;

namespace RapidPay.Services.Interfaces
{
    public interface ICardService
    {
        Task<CardViewModel> GetCardAsync(Guid userId);
        Task<CardViewModel> CreateCardAsync(Guid userId, CardCreateViewModel viewModel);
        Task UpdateCardAsync(Guid userId, CardUpdateViewModel viewModel);
        Task<bool> PayAsync(Guid userId, decimal amount);
    }
}
