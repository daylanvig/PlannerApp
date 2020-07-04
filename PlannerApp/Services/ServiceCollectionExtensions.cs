
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace PlannerApp.Client.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerAppServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorizedHttpClientFactory, AuthorizedHttpClientFactory>();
            services.AddTransient<IPlannerItemDataService, PlannerItemDataService>();
            services.AddTransient<IPlannerItemService, PlannerItemService>();
            services.AddTransient<ICategoryDataService, CategoryDataService>();


            return services;
        }

    }
}
