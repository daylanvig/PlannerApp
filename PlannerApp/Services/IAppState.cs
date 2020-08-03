using PlannerApp.Client.Store.ChangePageUseCase;
using System;

namespace PlannerApp.Client.Services
{
    public interface IAppState
    {
        NavMenuState Title { get; }
        string GetCurrentPath();

        event Action OnAppStateChange;

        void UpdateTitle(NavMenuState titleState = null);
    }
}