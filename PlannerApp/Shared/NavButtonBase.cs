﻿using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;

namespace PlannerApp.Client.Shared
{
    public class NavButtonBase : ComponentBase
    {
        [Inject] IAppState AppState { get; set; }
        [Parameter]
        public string Path { get; set; }
        private string label;
        [Parameter]
        public string Label
        {
            get
            {
                if (string.IsNullOrEmpty(label))
                {
                    return Path.Substring(1); // remove the leading slash
                }
                return label;
            }
            set
            {
                label = value;
            }
        }

        protected string GetClasses()
        {
            var navPath = Path.Replace("/", string.Empty);
            return $"nav__button {(string.Equals(navPath, AppState.GetCurrentPath(), System.StringComparison.InvariantCultureIgnoreCase) ? "nav__button--active" : string.Empty)}";
        }
    }
}
