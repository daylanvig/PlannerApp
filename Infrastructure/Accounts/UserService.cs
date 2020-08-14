using Application.Interfaces.Infrastructure;
using Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Accounts
{
    public class UserService : IUserService
    {
        private readonly UserManager<PlannerAppUser> userManager;
        private readonly SignInManager<PlannerAppUser> signInManager;
        // todo -> move these to wrapper
        public UserService(UserManager<PlannerAppUser> userManager, SignInManager<PlannerAppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUser(PlannerAppUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>The user with that email/password if is successful, else null</returns>
        public async Task<PlannerAppUser> SignIn(string email, string password)
        {
            var signInResult = await signInManager.PasswordSignInAsync(email, password, true, true);
            if (!signInResult.Succeeded)
            {
                return null;
            }
            return await userManager.FindByEmailAsync(email);
        }
    }
}
