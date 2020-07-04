using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.AccountComponents
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
