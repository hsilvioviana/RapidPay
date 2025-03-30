using RapidPay.Application.ViewModels;

namespace RapidPay.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationViewModel> SignUp(SignUpViewModel viewModel);
        Task<AuthenticationViewModel> Login(LoginViewModel viewModel);
        Task<AuthenticationViewModel> Update(Guid id, UpdateUserViewModel viewModel);
    }
}
