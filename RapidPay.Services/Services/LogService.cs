using AutoMapper;
using RapidPay.Services.Interfaces;

namespace RapidPay.Services.Services
{
    public class LogService : ILogService
    {
        private readonly IMapper _mapper;

        public LogService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task TrackChange(Guid cardId, string field, string from, string to)
        {
            await Task.Delay(1);
        }
    }
}
