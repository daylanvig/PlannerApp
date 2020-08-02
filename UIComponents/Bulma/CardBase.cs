using Microsoft.AspNetCore.Components;
using UIComponents.Bulma.Custom;

namespace UIComponents.Bulma
{
    public class CardBase : UIComponentBase
    {
        [Parameter]
        public RenderFragment CardHeader { get; set; }
        [Parameter]
        public RenderFragment CardContent { get; set; }
        [Parameter]
        public RenderFragment CardFooter { get; set; }
    }
}
