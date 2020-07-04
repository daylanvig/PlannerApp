using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Server.Data;
using PlannerApp.Server.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection ConfigurePlannerAppIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<PlannerAppUser>()
                    .AddRoles<PlannerAppRole>()
                    .AddEntityFrameworkStores<UserContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<PlannerAppUser, UserContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
