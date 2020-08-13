using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationClient.Components.DayPlannerComponents
{
    public class CalendarNowMarkerBase : ComponentBase
    {
        [Parameter]
        public DateTime ForDate { get; set; }

        protected string GetTop() => UIComponentHelper.CalculateTop(DateTime.Now);
    }
}
