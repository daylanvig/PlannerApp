using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.AccountComponents
{
    public class LoginFormBase : ComponentBase
    {
        [Parameter]
        public LoginModel LoginModel { get; set; } = new LoginModel();
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
