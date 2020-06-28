using Microsoft.Extensions.DependencyInjection;

namespace PlannerApp.Client.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerAppServices(this IServiceCollection services)
        {
            services.AddTransient<IPlannerItemDataService, PlannerItemDataService>();
            return services;
        }

    }
}
