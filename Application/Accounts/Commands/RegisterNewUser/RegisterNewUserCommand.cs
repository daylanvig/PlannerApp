using Application.Interfaces.Infrastructure;
using Domain.Accounts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Accounts.Commands.RegisterNewUser
{
    public class RegisterNewUserCommand : IRegisterNewUserCommand
    {
        private readonly IUserService userService;

        public RegisterNewUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<RegisterResult> Execute(RegisterModel registerModel)
        {
            var result = await userService.CreateUser(new PlannerAppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                TenantID = Guid.NewGuid().ToString()
            }, registerModel.Password);

            return new RegisterResult
            {
                IsSuccessful = result.Succeeded,
                Errors = result.Succeeded ? Array.Empty<string>() : result.Errors.Select(e => e.Description)
            };
        }
    }
}
