﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ClientApp.Services.ComponentHelperServices;
using UIComponents.Bulma.Modal;
using UIComponents.Custom.SheetComponent;
using UIComponents.Services;

namespace ClientApp.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerAppServices(this IServiceCollection services)
        {
            services.AddWebServices();
            services.AddUIComponentServices();
            services.AddDataServices();
            services.AddStateServices();
            services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDateTimeGlobalizationService, DateTimeGlobalizationService>();
            return services;
        }

        private static void AddStateServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppState, AppState>();
            services.AddScoped<ICalendarService, CalendarService>();
        }

        private static void AddWebServices(this IServiceCollection services)
        {
            services.AddSingleton<IPlannerItemComponentService, PlannerItemComponentService>();
            services.AddSingleton<ICategoryComponentService, CategoryComponentService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthorizedHttpClientFactory, AuthorizedHttpClientFactory>();
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
            services.AddDefaultUIComponentServices();
            return services;
        }

    }
}
