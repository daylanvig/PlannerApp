
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace PlannerApp.Client.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerAppServices(this IServiceCollection services)
        {
            services.AddTransient<IPlannerItemDataService, PlannerItemDataService>();
            services.AddTransient<IPlannerItemService, PlannerItemService>();
            services.AddTransient<ICategoryDataService, CategoryDataService>();

            services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

    }
}
