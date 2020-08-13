using Application.PlannerItems.Queries.Common;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace PresentationClient.Components.DashboardComponents
{
    public class EventListBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public IEnumerable<PlannerItemModel> Items { get; set; }
    }
}
