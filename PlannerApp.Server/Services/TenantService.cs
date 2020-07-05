using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlannerApp.Server.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlannerApp.Server.Services
{
    public interface ITenantService
    {
        string GetTenantID();
    }

    public class TenantService : ITenantService
    {
        private readonly UserManager<PlannerAppUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor, UserManager<PlannerAppUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public string GetTenantID()
        {
            var identity = httpContextAccessor.HttpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return "";
            }
            var user = userManager.FindByNameAsync(identity.Name).Result;
            return user.TenantID;
        }
    }
}
