using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using PresentationClient.Components;
using PresentationClient.Services;
using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationClient.Pages
{
    public class AuthenticationBase : ComponentBase
    {
        private string redirectUrl;
        [Inject]
        public IAppState AppState { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } 
        [Parameter]
        public string Action { get; set; }
        /// <summary>
        /// Current set to lowercase
        /// </summary>
        protected string ActionCased
        {
            get
            {
                if (string.IsNullOrEmpty(Action))
                {
                    return "";
                }
                return Action.ToLower();
            }
        }

        protected override void OnInitialized()
        {
            if (QueryHelpers.ParseQuery(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query).TryGetValue("returnUrl", out var returnUrl))
            {
                redirectUrl = returnUrl;
            }
            else
            {
                redirectUrl = "";
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            AppState.UpdateTitle(new Store.ChangePageUseCase.NavMenuState());
            if (ActionCased == "signout")
            {
                await AuthService.Logout();
                NavigationManager.NavigateTo("/authentication/signin");
            }
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected void ChangePage(string pageChangeTo)
        {
            Action = pageChangeTo;
            StateHasChanged();
        }

    }
}
