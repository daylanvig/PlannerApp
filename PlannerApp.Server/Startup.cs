using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlannerApp.Server.Data;
using PlannerApp.Server.Extensions;
using PlannerApp.Shared;
using System;
using System.Reflection;

namespace PlannerApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // stops app engine from redirecting infinitely
            services.Configure<ForwardedHeadersOptions>(o =>
            {
                o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                o.ForwardLimit = 2;
            });
            services.AddHttpsRedirection(o => o.HttpsPort = 443);
            services.AddHsts(o =>
            {
                o.Preload = true;
                o.IncludeSubDomains = true;
                o.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddDbContext<UserContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("UserContext"));
            });

            services.AddDbContext<PlannerContext>(o =>
            {

                o.UseSqlServer(Configuration.GetConnectionString("PlannerContext"));
            });
            services.AddHttpContextAccessor();
            services.ConfigurePlannerAppIdentity(Configuration);
            services.AddPlannerAppServerServices();
            
            services.AddControllersWithViews();
           
            services.AddRazorPages();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddPlannerAppSharedServices();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions() { ForwardedHeaders = ForwardedHeaders.XForwardedProto });
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
