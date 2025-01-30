using Accounts.Domain.Repositories;
using Accounts.Infrastructure.Data.Interfaces;
using Accounts.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppContext, Accounts.Infrastructure.Data.AppContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
