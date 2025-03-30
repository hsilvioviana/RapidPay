using AutoMapper;
using RapidPay.Domain.Interfaces;
using RapidPay.Services.Interfaces;

namespace RapidPay.Services.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
