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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            #endregion

            #region Services
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IFeeService, FeeService>();

            services.AddHostedService<FeeBackgroundService>();
            #endregion

            #region DbContexts
            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            #endregion
        }
    }
}
