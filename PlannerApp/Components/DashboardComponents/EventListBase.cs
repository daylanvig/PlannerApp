using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System.Collections.Generic;

namespace PlannerApp.Client.Components.DashboardComponents
{
    public class EventListBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<PlannerItemDTO> Items { get; set; }
    }
}
