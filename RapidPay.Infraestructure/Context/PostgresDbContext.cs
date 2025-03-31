using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Models;

namespace RapidPay.Infraestructure.Context
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        { 
            
        }

        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<CardModel> Cards => Set<CardModel>();
        public DbSet<FeeModel> Fees => Set<FeeModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
