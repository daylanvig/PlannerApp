using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using Domain.Accounts;

namespace Infrastructure.Accounts
{
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
