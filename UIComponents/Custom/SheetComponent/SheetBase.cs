using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
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
        private bool isOpen = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SheetService.OnShow += Show;
            SheetService.OnClose += Hide;
        }

        private void Show(SheetParams sheetParams)
        {
            Body = sheetParams.Body;
        }

        private void Hide()
        {
            isOpen = false;
            StateHasChanged();
        }

        protected string GetCssClasses()
        {
            return $"sheet {CssClass} {(isOpen ? "sheet--open" : null)}";
        }

        public void ToggleState()
        {
            isOpen = !isOpen;
            StateHasChanged();
        }

        public void Dispose()
        {
            SheetService.OnShow -= Show;
            SheetService.OnClose -= Hide;
        }
    }
}
