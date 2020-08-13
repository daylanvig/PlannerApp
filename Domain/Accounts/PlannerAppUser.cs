using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts
{
    public class PlannerAppUser : IdentityUser<int>
    {
        public string TenantID { get; set; }
    }
}
