using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public  static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(assemblies: typeof(Services.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
