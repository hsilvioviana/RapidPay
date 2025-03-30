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

        public DbSet<CardModel> Card => Set<CardModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
