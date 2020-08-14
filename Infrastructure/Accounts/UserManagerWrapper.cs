using Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Accounts
{
    public class UserManagerWrapper : IUserManagerWrapper
    {
        private readonly UserManager<PlannerAppUser> userManager;

        public UserManagerWrapper(UserManager<PlannerAppUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<PlannerAppUser> FindByNameAsync(string userName)
        {
            return userManager.FindByNameAsync(userName);
        }
    }
}
