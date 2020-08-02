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
            services.AddWebServices();
            services.AddDataServices();
            services.AddStateServices();
            services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPlannerItemService, PlannerItemService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }

        private static void AddStateServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppState, AppState>();
            services.AddScoped<ICalendarService, CalendarService>();
        }

        private static void AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthorizedHttpClientFactory, AuthorizedHttpClientFactory>();
            services.AddTransient<IDOMInteropService, DOMInteropService>();
        }

        private static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDataService, CategoryDataService>();
            services.AddScoped<IPlannerItemDataService, PlannerItemDataService>();
        }

        public static IServiceCollection AddUIComponentServices(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationWideComponentService<ModalParams>, ApplicationWideComponentService<ModalParams>>();
            services.AddSingleton<IApplicationWideComponentService<SheetParams>, ApplicationWideComponentService<SheetParams>>();
            return services;
        }

    }
}
