using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UIComponents.Bulma.Custom;

namespace UIComponents.Bulma
{
    public class CheckBoxBase : UIComponentBase
    {
        protected bool IsChecked;
        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }
        [Parameter]
        public bool Checked { get => IsChecked; set { IsChecked = value; } }

        protected async Task ChangeToggleState(ChangeEventArgs e)
        {
            IsChecked = !IsChecked;
            await OnChange.InvokeAsync(e);
            StateHasChanged();
        }
    }
}
