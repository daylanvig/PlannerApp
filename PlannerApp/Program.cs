using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var host = builder.Build();
            host.Services
                .UseBulmaProviders()
                .UseFontAwesomeIcons();
            await host.RunAsync();
        }
    }
}
