using Microsoft.AspNetCore.Components;
using System;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarDateHeaderBase : ComponentBase
    {
        [Parameter]
        public DateTime Date { get; set; }
    }
}
