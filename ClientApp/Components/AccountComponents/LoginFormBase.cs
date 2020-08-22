using Application.Accounts.Commands.SignInUser;
using Microsoft.AspNetCore.Components;
using ClientApp.Services;
using System;
using System.Threading.Tasks;

namespace ClientApp.Components.AccountComponents
{
    public class LoginFormBase : ComponentBase
    {
        [Parameter]
        public SignInUserModel LoginModel { get; set; } = new SignInUserModel();
        [Parameter]
        public Action GoToRegisterAction { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }

        public async Task LogInToAccount()
        {
            await AuthService.Login(LoginModel);
            // auto navigates when auth state changes
        }
    }
}
