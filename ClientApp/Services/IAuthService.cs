using Application.Accounts.Commands.RegisterNewUser;
using Application.Accounts.Commands.SignInUser;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IAuthService
    {
        Task<SignInUserResult> Login(SignInUserModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<AuthenticationHeaderValue> GetAuthToken();
    }
}
