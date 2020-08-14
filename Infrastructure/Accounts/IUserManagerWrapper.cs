using Domain.Accounts;
using System.Threading.Tasks;

namespace Infrastructure.Accounts
{
    public interface IUserManagerWrapper
    {
        Task<PlannerAppUser> FindByNameAsync(string userName);
    }
}