using Application.Interfaces.Infrastructure;
using AutoFixture;
using Domain.Accounts;
using System;
using System.Collections.Generic;
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
            var response = new RegisterResult
            {
                IsSuccessful = result.Succeeded,
            };
            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            foreach(var error in result.Errors)
            {
                var field = error.Code.Contains("Password") ? nameof(RegisterModel.Password) : nameof(RegisterModel.Email);
                errors.Add(new KeyValuePair<string, string>(field, error.Description));
            }
            response.Errors = errors;
            return response;
        }
    }
}
