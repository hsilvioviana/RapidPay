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
    }
}
