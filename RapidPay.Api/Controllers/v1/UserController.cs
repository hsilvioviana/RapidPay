using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.Validations;
using RapidPay.Application.ViewModels;
using RapidPay.Services.Interfaces;

namespace RapidPay.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        protected readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // POST api/users/signup
        /// <summary>
        /// User registration.
        /// </summary>
        /// <remarks>
        /// Example body:
        /// 
        ///     {
        ///         "username": "john123",
        ///         "email": "john123@email.com",
        ///         "password": "123456"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Registration data.</param>
        /// <response code="200">Registration completed successfully.</response>
        /// <response code="400">Error during registration.</response>
        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<AuthenticationViewModel> Signup([FromBody] SignUpViewModel viewModel)
        {
            ValidationHelper.Validate(new SignUpValidation(), viewModel);

            return await _service.SignUp(viewModel);
        }

        // POST api/users/login
        /// <summary>
        /// User login.
        /// </summary>
        /// <remarks>
        /// Example body:
        /// 
        ///     {
        ///         "username": "john123",
        ///         "password": "123456"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Login data.</param>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Login failed.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<AuthenticationViewModel> Login([FromBody] LoginViewModel viewModel)
        {
            ValidationHelper.Validate(new LoginValidation(), viewModel);

            return await _service.Login(viewModel);
        }

        // PUT api/users/update
        /// <summary>
        /// Update user information.
        /// </summary>
        /// <remarks>
        /// Example body:
        /// 
        ///     {
        ///         "username": "john123",
        ///         "email": "john123@email.com",
        ///         "password": "123456",
        ///         "newPassword": "1234567"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Update data.</param>
        /// <response code="200">Update successful.</response>
        /// <response code="400">Update failed.</response>
        [HttpPut("update")]
        [Authorize]
        public async Task<AuthenticationViewModel> Update([FromBody] UpdateUserViewModel viewModel)
        {
            ValidationHelper.Validate(new UpdateUserValidation(), viewModel);

            return await _service.Update(UserId, viewModel);
        }
    }
}
