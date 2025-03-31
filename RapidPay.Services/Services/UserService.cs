using AutoMapper;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Services.Interfaces;
using RapidPay.Application.Utils;
using RapidPay.Application.Validations;
using RapidPay.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using RapidPay.Application.CustomExceptions;
using static RapidPay.Application.Validations.ValidationHelper;

namespace RapidPay.Services.Services
{
    public class UserService : IUserService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IUserRepository _repository;
        protected readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IUserRepository repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthenticationViewModel> SignUpAsync(SignUpViewModel viewModel)
        {
            var model = _mapper.Map<UserModel>(viewModel);

            var checkUsername = await _repository.SearchByUsernameAsync(model.Username);

            ThrowErrorWhen(checkUsername, ComparisonType.NotEqual, null, new InvalidInputException(ErrorMessages.Authentication.UsernameAlreadyInUse));

            var checkEmail = await _repository.SearchByEmailAsync(model.Email);

            ThrowErrorWhen(checkEmail, ComparisonType.NotEqual, null, new InvalidInputException(ErrorMessages.Authentication.EmailAlreadyInUse));

            model.Password = Security.Hash(model.Password);
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(model);

            return new AuthenticationViewModel()
            {
                Username = model.Username,
                Email = model.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(model))
            };
        }

        public async Task<AuthenticationViewModel> LoginAsync(LoginViewModel viewModel)
        {
            var requestUser = _mapper.Map<UserModel>(viewModel);

            var user = await _repository.SearchByUsernameAsync(requestUser.Username);

            ThrowErrorWhen(user, ComparisonType.Equal, null, new EntityNotFoundException(ErrorMessages.Authentication.UserNotFound));

            var correctPassword = Security.Check(user.Password, requestUser.Password);

            ThrowErrorWhen(correctPassword,ComparisonType.Equal, false, new InvalidInputException(ErrorMessages.Authentication.IncorrectPassword));

            return new AuthenticationViewModel()
            {
                Username = user.Username,
                Email = user.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }

        public async Task<AuthenticationViewModel> UpdateAsync(Guid id, UpdateUserViewModel viewModel)
        {
            var user = await _repository.FindAsync(id);

            ThrowErrorWhen(user,ComparisonType.Equal, null, new EntityNotFoundException(ErrorMessages.Authentication.UserNotFound));

            var checkUsername = await _repository.SearchByUsernameAsync(viewModel.Username);

            if (checkUsername?.Id != user.Id)
                ThrowErrorWhen(checkUsername, ComparisonType.NotEqual, null, new InvalidInputException(ErrorMessages.Authentication.UsernameAlreadyInUse));

            var checkEmail = await _repository.SearchByEmailAsync(viewModel.Email);

            if (checkEmail?.Id != user.Id)
                ThrowErrorWhen(checkEmail, ComparisonType.NotEqual, null, new InvalidInputException(ErrorMessages.Authentication.EmailAlreadyInUse));

            if (!string.IsNullOrEmpty(viewModel.Password) && !string.IsNullOrEmpty(viewModel.NewPassword))
            {
                var correctPassword = Security.Check(user.Password, viewModel.Password);

                ThrowErrorWhen(correctPassword, ComparisonType.Equal, false, new InvalidInputException(ErrorMessages.Authentication.IncorrectPassword));

                user.Password = Security.Hash(viewModel.NewPassword);
            }

            user.Username = viewModel.Username;
            user.Email = viewModel.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(user);

            return new AuthenticationViewModel()
            {
                Username = user.Username,
                Email = user.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }
    }
}