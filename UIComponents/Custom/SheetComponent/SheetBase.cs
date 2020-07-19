using Microsoft.AspNetCore.Components;
using System;
using UIComponents.Bulma.Custom;
using UIComponents.Services;

namespace UIComponents.Custom.SheetComponent
{
    /// <summary>
    /// Side sheet component
    /// </summary>
    /// <remarks>
    /// Create a side element that pops in and out to display menu content.
    /// Use with <see cref="UIComponents.Services.IApplicationWideComponentService{T}" to handle visiblity/>
    /// Component should be placed at top level of DOM structure
    /// </remarks>
    /// <seealso cref="https://material.io/components/sheets-side#placement"/>
    public class SheetBase : UIComponentBase, IDisposable
    {
        [Inject] IApplicationWideComponentService<SheetParams> SheetService { get; set; }
        protected RenderFragment Body;
        protected bool IsOpen = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SheetService.OnShow += Show;
            SheetService.OnClose += Hide;
        }

        private void Show(SheetParams sheetParams)
        {
            Body = sheetParams.Body;
            IsOpen = true;
            StateHasChanged();
        }

        protected void Hide()
        {
            IsOpen = false;
            StateHasChanged();
        }

        protected string GetCssClasses()
        {
            return $"sheet {CssClass} {(IsOpen ? "sheet--open" : null)}";
        }

        public void ToggleState()
        {
            IsOpen = !IsOpen;
            StateHasChanged();
        }

        public void Dispose()
        {
            SheetService.OnShow -= Show;
            SheetService.OnClose -= Hide;
        }
    }
}
