using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PresentationClient.Services;
using System;
using PlannerApp.Shared;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Application.PlannerItems.Queries.Common;
using Application.PlannerItems.Commands.Shared;
using System.Reflection;

namespace PresentationClient
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<PlannerItemModel, PlannerItemCreateEditModel>();
        }
    }
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddUIComponentServices();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(Application.Config)));
            builder.Services.AddPlannerAppSharedServices();
            builder.Services.AddPlannerAppServices();
            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
