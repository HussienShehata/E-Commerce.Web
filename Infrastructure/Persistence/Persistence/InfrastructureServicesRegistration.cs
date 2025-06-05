

using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
           Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "DefaultConnection"));
            });
           Services.AddScoped<IDataSeeding, DataSeeding>();
           Services.AddScoped<IUnitOfWork, UnitOfWork>();
           Services.AddScoped<IBasketRepository, BasketRepository>();
           Services.AddSingleton<IConnectionMultiplexer>( (_) =>
           {
              return  ConnectionMultiplexer.Connect(configuration: Configuration.GetConnectionString(name: "RedisConnectionString"));
           });
            return Services;
        }
    }
}
