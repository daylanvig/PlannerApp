using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Shared.Services;

namespace PlannerApp.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerAppSharedServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
