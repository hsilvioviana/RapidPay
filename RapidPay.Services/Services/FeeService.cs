using Microsoft.Extensions.Caching.Memory;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Services.Interfaces;

namespace RapidPay.Services.Services
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly object _lock = new();

        private const string CACHE_KEY = "CurrentFee";

        public FeeService(IFeeRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public decimal GetCurrentFee()
        {
            if (_cache.TryGetValue(CACHE_KEY, out decimal fee))
            {
                return fee;
            }

            var currentFee = _repository.GetLastAsync().GetAwaiter().GetResult();
            _cache.Set(CACHE_KEY, currentFee.Value);

            return currentFee.Value;
        }

        public async Task UpdateFeeAsync()
        {
            lock (_lock)
            {
                var currentFee = _repository.GetLastAsync().GetAwaiter().GetResult();
                var multiplier = new Random().NextDouble() * 2;
                var newFee = Math.Round(currentFee.Value * (decimal)multiplier, 2);

                _cache.Set(CACHE_KEY, newFee);

                _repository.CreateAsync(new FeeModel
                {
                    Value = newFee,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).GetAwaiter().GetResult();
            }
        }
    }
}