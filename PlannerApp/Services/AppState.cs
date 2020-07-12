using PlannerApp.Client.Store.ChangePageUseCase;
using System;
using UIComponents.Bulma;

namespace PlannerApp.Client.Components
{
    public class AppState
    {
        public TitleState Title { get; private set; } = new TitleState();
        public event Action OnAppStateChange;

        public void UpdateTitle(TitleState titleState = null)
        {
            Title = titleState ?? new TitleState();
            NotifyStateChanged();
        }
        
        private void NotifyStateChanged()
        {
            OnAppStateChange.Invoke();
        }
    }
}
