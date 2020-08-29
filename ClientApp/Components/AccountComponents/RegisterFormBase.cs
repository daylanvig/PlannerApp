using Application.Accounts.Commands.RegisterNewUser;
using ClientApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UIComponents;

namespace ClientApp.Components.AccountComponents
{
    public class RegisterFormBase : ComponentBase
    {
        [Parameter]
        public Action GoToLoginAction { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        public RegisterModel RegisterModel { get; set; } = new RegisterModel();

        protected FluentValidator<RegisterModelValidator> Validator;
        protected async Task RegisterUser()
        {
            var result = await AuthService.Register(RegisterModel);
            if (result.IsSuccessful)
            {
                GoToLoginAction.Invoke();
            }
            else
            {
                Validator.DisplayErrors(result.Errors);
            }
        }
    }
}
