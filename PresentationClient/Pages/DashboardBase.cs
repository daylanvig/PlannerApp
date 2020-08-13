using Microsoft.AspNetCore.Components;
using PresentationClient.Services;
using PresentationClient.Store.ChangePageUseCase;

namespace PresentationClient.Pages
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
