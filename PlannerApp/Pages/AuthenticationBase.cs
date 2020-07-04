using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Pages
{
    public class AuthenticationBase : ComponentBase
    {
        private string redirectUrl;
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Action { get; set; }
        protected string ActionCased
        {
            get
            {
                if (string.IsNullOrEmpty(Action))
                {
                    return "";
                }
                return Action.ToLower();
            }
        }

        protected void ChangePage(string pageChangeTo)
        {
            Console.WriteLine(pageChangeTo);
            Action = pageChangeTo;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            if (QueryHelpers.ParseQuery(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query).TryGetValue("returnUrl", out var returnUrl))
            {
                redirectUrl = returnUrl;
            }
            else
            {
                redirectUrl = "";
            }
        }

    }
}
