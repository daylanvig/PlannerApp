using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UIComponents.Bulma.Modal;
using UIComponents.Custom.SheetComponent;
using UIComponents.Services;

namespace PlannerApp.Client.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppState, AppState>();
            services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorizedHttpClientFactory, AuthorizedHttpClientFactory>();
            services.AddTransient<IPlannerItemDataService, PlannerItemDataService>();
            services.AddTransient<IPlannerItemService, PlannerItemService>();
            services.AddTransient<ICategoryDataService, CategoryDataService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddTransient<IDOMInteropService, DOMInteropService>();
            return services;
        }

        public static IServiceCollection AddUIComponentServices(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationWideComponentService<ModalParams>, ApplicationWideComponentService<ModalParams>>();
            services.AddSingleton<IApplicationWideComponentService<SheetParams>, ApplicationWideComponentService<SheetParams>>();
            return services;
        }

    }
}
