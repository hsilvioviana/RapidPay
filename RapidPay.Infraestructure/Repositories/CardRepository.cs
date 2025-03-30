using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Infraestructure.Context;

namespace RapidPay.Infraestructure.Repositories
{
    public class CardRepository : BaseRepository<CardModel>, ICardRepository
    {
        protected readonly PostgresDbContext _context;

        public CardRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<CardModel> GetByUserIdAsync(Guid userId)
        {
            return _context.Cards.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
