using AutoMapper;
using RapidPay.Services.Interfaces;

namespace RapidPay.Services.Services
{
    public class FeeService : IFeeService
    {
        private readonly IMapper _mapper;

        public FeeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<decimal> GetCurrentFee()
        {
            return Task.FromResult(2.51m);
        }
    }
}
