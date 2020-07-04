using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PlannerApp.Client.Components;
using PlannerApp.Client.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Auth0;

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
            builder.Services.AddPlannerAppServices();

            builder.Services.AddBlazorAuth0(o =>
            {
                o.Domain = "dev-oso--pz1.us.auth0.com";
                o.Audience = "LO2n5OazAzyyJ0K1Spf5lpAP2aTJ6SVv";
            });

            builder.Services.AddAuthorizationCore();

            var host = builder.Build();
            host.Services
                .UseBulmaProviders()
                .UseFontAwesomeIcons();
            await host.RunAsync();
        }
    }
}
