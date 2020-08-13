using System.Threading.Tasks;

namespace Application.Accounts.Commands.RegisterNewUser
{
    public interface IRegisterNewUserCommand
    {
        Task<RegisterResult> Execute(RegisterModel registerModel);
    }
}