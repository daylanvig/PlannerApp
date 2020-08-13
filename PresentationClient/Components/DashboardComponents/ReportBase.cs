using Microsoft.AspNetCore.Components;

namespace PresentationClient.Components.DashboardComponents
{
    public class ReportBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public bool IsLoading { get; set; }
    }
}
