using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Infraestructure.Context;

namespace RapidPay.Infraestructure.Repositories
{
    public class FeeRepository : BaseRepository<FeeModel>, IFeeRepository
    {
        protected readonly PostgresDbContext _context;

        public FeeRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<FeeModel> GetLastAsync()
        {
            return _context.Fees.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }
    }
}
