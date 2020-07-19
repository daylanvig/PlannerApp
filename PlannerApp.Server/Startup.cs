using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.ApiAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlannerApp.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using AspNet.Security.OpenIdConnect.Primitives;
using PlannerApp.Server.Extensions;
using PlannerApp.Client.Services;
using PlannerApp.Server.Services;
using PlannerApp.Shared;

namespace PlannerApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
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
                o.UseMySql(Configuration.GetConnectionString("UserContext"));
            });
            
            services.AddDbContext<PlannerContext>(o =>
            {
                o.UseMySql(Configuration.GetConnectionString("PlannerContext"));
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
