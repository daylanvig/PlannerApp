using Microsoft.AspNetCore.Components;
using ClientApp.Services;
using Application.Accounts.Commands.RegisterNewUser;
using System;
using System.Threading.Tasks;

namespace ClientApp.Components.AccountComponents
{
    public class RegisterFormBase : ComponentBase
    {
        [Parameter]
        public Action GoToLoginAction { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        public RegisterModel RegisterModel { get; set; } = new RegisterModel();
 
        protected async Task RegisterUser()
        {
            var result = await AuthService.Register(RegisterModel);
            if (result.IsSuccessful)
            {
                GoToLoginAction.Invoke();
            }
            else
            {
                // todo
            }
        }
    }
}
