using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Client.Components;
using PlannerApp.Client.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));
            builder.Services.AddAuthorizationCore();
            builder.Services.AddUIComponentServices();
            builder.Services.AddPlannerAppServices();

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
