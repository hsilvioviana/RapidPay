using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;
using RapidPay.Infraestructure.Context;

namespace RapidPay.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        protected readonly PostgresDbContext _context;

        public UserRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserModel> SearchByUsername(string username)
        {
            return await DbSet.Where(U => U.Username == username).FirstOrDefaultAsync();
        }

        public async Task<UserModel> SearchByEmail(string email)
        {
            return await DbSet.Where(U => U.Email == email).FirstOrDefaultAsync();
        }
    }
}
