

using Microsoft.Extensions.Configuration;

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

            return Services;
        }
    }
}
