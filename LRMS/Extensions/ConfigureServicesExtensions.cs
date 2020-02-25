using Microsoft.Extensions.DependencyInjection;
using LRMS.Contracts;
using LRMS.Services;

namespace LRMS.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void ConfigureAllDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IStatusService, StatusService>();
        }

    }
}
