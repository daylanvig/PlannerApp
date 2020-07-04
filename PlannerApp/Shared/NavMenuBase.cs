using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public AppState AppState { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            AppState.OnPageChange += UpdateTitle;
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
