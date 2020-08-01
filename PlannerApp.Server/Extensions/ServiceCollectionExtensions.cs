using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlannerApp.Server.Data;
using PlannerApp.Server.Models.Identity;
using PlannerApp.Server.Services;
using PlannerApp.Shared.Services;
using System.Text;

namespace PlannerApp.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePlannerAppIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<PlannerAppUser>()
                    .AddRoles<PlannerAppRole>()
                    .AddEntityFrameworkStores<UserContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtIssuer"],
                        ValidAudience = configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]))
                    };
                });

            return services;
        }

        public static IServiceCollection AddPlannerAppServerServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IPlannerItemRepository, PlannerItemRepository>();
            return services;
        }
    }
}
