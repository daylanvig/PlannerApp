using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarColumnsBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private List<CalendarColumn> Columns = new List<CalendarColumn>();

    }
}
