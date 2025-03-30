using AutoMapper;
using RapidPay.Application.CustomExceptions;
using RapidPay.Application.Utils;
using RapidPay.Application.Validations;
using RapidPay.Application.ViewModels;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Services.Interfaces;

namespace RapidPay.Services.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;
        private readonly IFeeService _feeService;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public CardService(ICardRepository repository, IFeeService feeService, ILogService logService, IMapper mapper)
        {
            _repository = repository;
            _feeService = feeService;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<CardViewModel> CreateCardAsync(Guid userId, CardCreateViewModel request)
        {
            var currentCard = await _repository.GetByUserIdAsync(userId);
            ValidationHelper.ThrowErrorWhen(currentCard, "NotEqual", null, new InvalidInputException(ErrorMessages.Card.AlreadyExists));

            var number = GenerateCardNumber();
            var cvv = GenerateCVV();
            var expiration = DateTime.UtcNow.AddYears(8);
            var balance = new Random().Next(100, 1000);

            var card = new CardModel
            {
                UserId = userId,
                Number = Security.Encrypt(number),
                CVV = Security.Encrypt(cvv),
                ExpirationDate = expiration,
                Balance = balance,
                CreditLimit = request.CreditLimit,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(card);

            return new CardViewModel
            {
                Number = number,
                CVV = cvv,
                ExpirationDate = expiration.ToString("MM/yy"),
                Balance = card.Balance,
                CreditLimit = card.CreditLimit,
                Active = card.Active
            };
        }

        public async Task<CardViewModel> GetCardAsync(Guid userId)
        {
            var card = await _repository.GetByUserIdAsync(userId);
            ValidationHelper.ThrowErrorWhen(card, "Equal", null, new InvalidInputException(ErrorMessages.Card.NotFound));

            var number = Security.Decrypt(card.Number);
            var cvv = Security.Decrypt(card.CVV);

            return new CardViewModel
            {
                Number = number,
                CVV = cvv,
                ExpirationDate = card.ExpirationDate.ToString("MM/yy"),
                Balance = card.Balance,
                CreditLimit = card.CreditLimit,
                Active = card.Active
            };
        }

        public async Task<bool> PayAsync(Guid userId, decimal amount)
        {
            var card = await _repository.GetByUserIdAsync(userId);

            ValidationHelper.ThrowErrorWhen(card, "Equal", null, new InvalidInputException(ErrorMessages.Card.NotFound));

            var fee = await _feeService.GetCurrentFee();
            var total = amount + fee;

            var available = card.Balance + (card.CreditLimit ?? 0);

            if (available < total)
                throw new InvalidInputException(ErrorMessages.Card.InsufficientBalance);

            card.Balance -= total;
            await _repository.UpdateAsync(card);

            return true;
        }

        public async Task UpdateCardAsync(Guid userId, CardUpdateViewModel request)
        {
            var card = await _repository.GetByUserIdAsync(userId);
            ValidationHelper.ThrowErrorWhen(card, "Equal", null, new InvalidInputException(ErrorMessages.Card.NotFound));

            if (request.Balance.HasValue && request.Balance.Value != card.Balance)
            {
                await _logService.TrackChange(card.Id, "Balance", card.Balance.ToString(), request.Balance.Value.ToString());
                card.Balance = request.Balance.Value;
            }

            if (request.CreditLimit.HasValue && request.CreditLimit.Value != card.CreditLimit)
            {
                await _logService.TrackChange(card.Id, "CreditLimit", (card.CreditLimit?.ToString() ?? "null"), request.CreditLimit.Value.ToString());
                card.CreditLimit = request.CreditLimit.Value;
            }

            if (request.Active.HasValue && request.Active.Value != card.Active)
            {
                await _logService.TrackChange(card.Id, "Active", card.Active.ToString(), request.Active.Value.ToString());
                card.Active = request.Active.Value;
            }

            await _repository.UpdateAsync(card);
        }

        private string GenerateCardNumber()
        {
            var random = new Random();
            var number = random.NextInt64(100000000000000, 999999999999999).ToString();

            return $"{number.Substring(0, 4)} {number.Substring(4, 6)} {number.Substring(10, 5)}";
        }

        private string GenerateCVV()
        {
            return new Random().Next(1000, 9999).ToString();
        }
    }
}
