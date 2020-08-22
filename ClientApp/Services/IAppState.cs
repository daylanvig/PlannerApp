using ClientApp.Store.ChangePageUseCase;
using System;

namespace ClientApp.Services
{
    public interface IAppState
    {
        NavMenuState Title { get; }
        string GetCurrentPath();

        event Action OnAppStateChange;

        void UpdateTitle(NavMenuState titleState = null);
    }
}