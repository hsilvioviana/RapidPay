using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.Validations;
using RapidPay.Application.ViewModels;
using RapidPay.Services.Interfaces;

namespace RapidPay.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cards")]
    [ApiVersion("1.0")]
    public class CardController : BaseController
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }

        // POST api/cards
        /// <summary>
        /// Create a new card for the authenticated user.
        /// </summary>
        /// <remarks>
        /// Example body:
        /// 
        ///     {
        ///         "creditLimit": 500.0
        ///     }
        /// </remarks>
        /// <param name="viewModel">Card creation data.</param>
        /// <response code="200">Card created successfully.</response>
        /// <response code="400">Card already exists or input is invalid.</response>
        [HttpPost]
        [Authorize]
        public async Task<CardViewModel> Create([FromBody] CardCreateViewModel viewModel)
        {
            ValidationHelper.Validate(new CardCreateValidation(), viewModel);
            return await _service.CreateCardAsync(UserId, viewModel);
        }

        // GET api/cards
        /// <summary>
        /// Retrieve the current user's card information.
        /// </summary>
        /// <response code="200">Card found.</response>
        /// <response code="400">Card not found.</response>
        [HttpGet]
        [Authorize]
        public async Task<CardViewModel> Get()
        {
            return await _service.GetCardAsync(UserId);
        }

        // PUT api/cards
        /// <summary>
        /// Update card details (balance, credit limit, or status).
        /// </summary>
        /// <remarks>
        /// Example body:
        /// 
        ///     {
        ///         "balance": 1200.50,
        ///         "creditLimit": 800,
        ///         "active": true
        ///     }
        /// </remarks>
        /// <param name="viewModel">Fields to update.</param>
        /// <response code="200">Card updated successfully.</response>
        /// <response code="400">Card not found or invalid input.</response>
        [HttpPut]
        [Authorize]
        public async Task Update([FromBody] CardUpdateViewModel viewModel)
        {
            ValidationHelper.Validate(new CardUpdateValidation(), viewModel);
            await _service.UpdateCardAsync(UserId, viewModel);
        }

        // POST api/cards/pay
        /// <summary>
        /// Make a payment using the card.
        /// </summary>
        /// <remarks>
        /// Example body:
        /// 
        ///     {
        ///         "amount": 100.00
        ///     }
        /// </remarks>
        /// <param name="viewModel">Payment data containing the amount to be charged.</param>
        /// <response code="200">Payment successful.</response>
        /// <response code="400">Insufficient balance or card not found.</response>
        [HttpPost("pay")]
        [Authorize]
        public async Task<bool> Pay([FromBody] CardPaymentViewModel viewModel)
        {
            ValidationHelper.Validate(new CardPaymentValidation(), viewModel);
            return await _service.PayAsync(UserId, viewModel.Amount);
        }
    }
}
