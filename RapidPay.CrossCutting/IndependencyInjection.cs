using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RapidPay.Domain.Interfaces;
using RapidPay.Infraestructure.Context;
using RapidPay.Infraestructure.Repositories;
using RapidPay.Services.Interfaces;
using RapidPay.Services.Services;

namespace RapidPay.CrossCutting
{
    public static class DependencyInjection
    {
        public static void AddDependencies (this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddScoped<ICardRepository, CardRepository>();
            #endregion

            #region Services
            services.AddScoped<ICardService, CardService>();
            #endregion

            #region DbContexts
            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            #endregion
        }
    }
}
