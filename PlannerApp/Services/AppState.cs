﻿using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Store.ChangePageUseCase;
using System;

namespace PlannerApp.Client.Services
{
    public class AppState : IAppState
    {
        readonly NavigationManager navigationManager;
        public NavMenuState Title { get; private set; } = new NavMenuState();
        public event Action OnAppStateChange;

        public AppState(NavigationManager navigationManager)
        {
           this.navigationManager = navigationManager;
        }
        public string GetCurrentPath()
        {
            return navigationManager.ToBaseRelativePath(navigationManager.Uri);
        }
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
