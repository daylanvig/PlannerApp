using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using UIComponents.Bulma.Helpers;

namespace UIComponents.Bulma
{
    public class ButtonBase : UIComponentBase
    {
        [Parameter]
        public ComponentColour? Colour { get; set; }
        [Parameter]
        public bool IsDisabled { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        [Parameter]
        public string Type { get; set; } = "button";
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
