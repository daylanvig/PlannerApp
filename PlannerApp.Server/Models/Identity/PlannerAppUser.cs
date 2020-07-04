using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Server.Models.Identity
{
    public class PlannerAppUser : IdentityUser<int>
    {
    }
    
    public class PlannerAppRole : IdentityRole<int>
    {

    }
}
