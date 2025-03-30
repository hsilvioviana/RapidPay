using AutoMapper;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Services.Interfaces;
using RapidPay.Application.Utils;
using RapidPay.Application.Validations;
using RapidPay.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using RapidPay.Application.CustomExceptions;

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

        public async Task<AuthenticationViewModel> SignUp(SignUpViewModel viewModel)
        {
            var model = _mapper.Map<UserModel>(viewModel);

            var checkUsername = await _repository.SearchByUsername(model.Username);

            ValidationHelper.ThrowErrorWhen(checkUsername, "NotEqual", null, new InvalidInputException(ErrorMessages.Authentication.UsernameAlreadyInUse));

            var checkEmail = await _repository.SearchByEmail(model.Email);

            ValidationHelper.ThrowErrorWhen(checkEmail, "NotEqual", null, new InvalidInputException(ErrorMessages.Authentication.EmailAlreadyInUse));

            model.Password = Security.Hash(model.Password);
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);

            return new AuthenticationViewModel()
            {
                Username = model.Username,
                Email = model.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(model))
            };
        }

        public async Task<AuthenticationViewModel> Login(LoginViewModel viewModel)
        {
            var requestUser = _mapper.Map<UserModel>(viewModel);

            var user = await _repository.SearchByUsername(requestUser.Username);

            ValidationHelper.ThrowErrorWhen(user, "Equal", null, new EntityNotFoundException(ErrorMessages.Authentication.UserNotFound));

            var correctPassword = Security.Check(user.Password, requestUser.Password);

            ValidationHelper.ThrowErrorWhen(correctPassword, "Equal", false, new InvalidInputException(ErrorMessages.Authentication.IncorrectPassword));

            return new AuthenticationViewModel()
            {
                Username = user.Username,
                Email = user.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }

        public async Task<AuthenticationViewModel> Update(Guid id, UpdateUserViewModel viewModel)
        {
            var user = await _repository.Find(id);

            ValidationHelper.ThrowErrorWhen(user, "Equal", null, new EntityNotFoundException(ErrorMessages.Authentication.UserNotFound));

            var checkUsername = await _repository.SearchByUsername(viewModel.Username);

            if (checkUsername?.Id != user.Id)
                ValidationHelper.ThrowErrorWhen(checkUsername, "NotEqual", null, new InvalidInputException(ErrorMessages.Authentication.UsernameAlreadyInUse));

            var checkEmail = await _repository.SearchByEmail(viewModel.Email);

            if (checkEmail?.Id != user.Id)
                ValidationHelper.ThrowErrorWhen(checkEmail, "NotEqual", null, new InvalidInputException(ErrorMessages.Authentication.EmailAlreadyInUse));

            if (!string.IsNullOrEmpty(viewModel.Password) && !string.IsNullOrEmpty(viewModel.NewPassword))
            {
                var correctPassword = Security.Check(user.Password, viewModel.Password);

                ValidationHelper.ThrowErrorWhen(correctPassword, "Equal", false, new InvalidInputException(ErrorMessages.Authentication.IncorrectPassword));

                user.Password = Security.Hash(viewModel.NewPassword);
            }

            user.Username = viewModel.Username;
            user.Email = viewModel.Email;
            user.UpdatedAt = DateTime.Now;

            await _repository.Update(user);

            return new AuthenticationViewModel()
            {
                Username = user.Username,
                Email = user.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }
    }
}