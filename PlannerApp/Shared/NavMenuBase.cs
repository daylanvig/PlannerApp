using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Client.Components;
using System;
using System.Threading.Tasks;

namespace PlannerApp.Client.Shared
{
    public class NavMenuBase : ComponentBase, IDisposable
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Inject]
        public AppState AppState { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected string Title;
        protected string SubTitle;
        protected bool? IsUserAuthenticated { get; set; }
        protected override async Task OnInitializedAsync()
        {
            AppState.OnAppStateChange += UpdateTitle;
            await UpdateAuthStatus();
        }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateAuthStatus();
        }

        private async Task UpdateAuthStatus()
        {
            var isAuthenticated = (await AuthenticationStateTask).User.Identity.IsAuthenticated;
            if (!IsUserAuthenticated.HasValue || (IsUserAuthenticated.HasValue && IsUserAuthenticated.Value != isAuthenticated))
            {
                IsUserAuthenticated = isAuthenticated;
                StateHasChanged();
            }  
        }

        private void UpdateTitle()
        {
            Title = AppState.Title.Title;
            SubTitle = AppState.Title.SubTitle;
            StateHasChanged();
        }

        protected string CheckButtonClassActiveStatus(string buttonUrl)
        {
            return NavigationManager.ToBaseRelativePath(NavigationManager.Uri) == buttonUrl ? "nav__button--active" : string.Empty;
        }

        public void Dispose()
        {
            AppState.OnAppStateChange -= UpdateTitle;
        }
    }
}
