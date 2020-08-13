using Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Interfaces.Infrastructure
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(PlannerAppUser user, string password);
        Task<PlannerAppUser> SignIn(string email, string password);
    }
}