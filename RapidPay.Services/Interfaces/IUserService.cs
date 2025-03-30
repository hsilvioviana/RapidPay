using RapidPay.Application.ViewModels;

namespace RapidPay.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationViewModel> SignUpAsync(SignUpViewModel viewModel);
        Task<AuthenticationViewModel> LoginAsync(LoginViewModel viewModel);
        Task<AuthenticationViewModel> UpdateAsync(Guid id, UpdateUserViewModel viewModel);
    }
}
