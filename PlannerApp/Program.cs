using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Client.Components;
using PlannerApp.Client.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddBlazorise(o =>
            {
                o.ChangeTextOnKeyPress = true;
            })
            .AddBulmaProviders()
            .AddFontAwesomeIcons();
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));
            builder.Services.AddAuthorizationCore();
            builder.Services.AddPlannerAppServices();
            var host = builder.Build();
            host.Services
                .UseBulmaProviders()
                .UseFontAwesomeIcons();
            await host.RunAsync();
        }
    }
}
