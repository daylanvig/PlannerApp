using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Client.Components;
using PlannerApp.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        public AppState AppState { get; set; }
        
        [Parameter]
        public string Title { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected bool? IsUserAuthenticated { get; set; }
        protected override async Task OnInitializedAsync()
        {
            AppState.OnPageChange += UpdateTitle;
            await UpdateAuthStatus();
        }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateAuthStatus();
        }

        private async Task UpdateAuthStatus()
        {
            var isAuthenticated = (await authenticationStateTask).User.Identity.IsAuthenticated;
            if (!IsUserAuthenticated.HasValue || (IsUserAuthenticated.HasValue && IsUserAuthenticated.Value != isAuthenticated))
            {
                IsUserAuthenticated = isAuthenticated;
                StateHasChanged();
            }  
        }
        private void UpdateTitle()
        {
            Title = AppState.Title;
            StateHasChanged();
        }

        protected string CheckButtonClassActiveStatus(string buttonUrl)
        {
            return NavigationManager.ToBaseRelativePath(NavigationManager.Uri) == buttonUrl ? "nav__button--active" : string.Empty;
        }
    }
}
