using Microsoft.AspNetCore.Components;
using System;

namespace ClientApp.Components.DayPlannerComponents
{
    public class CalendarNowMarkerBase : ComponentBase
    {
        [Parameter]
        public DateTime ForDate { get; set; }

        protected string GetTop() => UIComponentHelper.CalculateTop(DateTime.Now);
    }
}
