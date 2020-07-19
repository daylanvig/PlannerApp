using PlannerApp.Client.Store.ChangePageUseCase;
using System;

namespace PlannerApp.Client.Services
{
    public class AppState : IAppState
    {
        public NavMenuState Title { get; private set; } = new NavMenuState();
        public event Action OnAppStateChange;

        public void UpdateTitle(NavMenuState titleState = null)
        {
            Title = titleState ?? new NavMenuState();
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnAppStateChange.Invoke();
        }
    }
}
