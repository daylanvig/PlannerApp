using System.Threading.Tasks;

namespace Application.Accounts.Commands.SignInUser
{
    public interface ISignInUserCommand
    {
        Task<SignInUserResult> Execute(SignInUserModel signInUserModel);
    }
}