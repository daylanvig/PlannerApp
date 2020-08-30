using Microsoft.Extensions.DependencyInjection;
using UIComponents.JSInterop.Services;

namespace UIComponents.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultUIComponentServices(this IServiceCollection services)
        {
            services.AddScoped<IDOMInteropService, DOMInteropService>();
            services.AddScoped<IJSDateHelperService, JSDateHelperService>();
            return services;
        }
    }
}
