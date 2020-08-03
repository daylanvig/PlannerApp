using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Client.Store.ChangePageUseCase;

namespace PlannerApp.Client.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject] IAppState AppState { get; set; }

        protected override void OnInitialized()
        {
            AppState.UpdateTitle(new NavMenuState("Dashboard"));
        }
    }
}
