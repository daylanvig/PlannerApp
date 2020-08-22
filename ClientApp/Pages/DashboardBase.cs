using ClientApp.Services;
using ClientApp.Store.ChangePageUseCase;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages
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
