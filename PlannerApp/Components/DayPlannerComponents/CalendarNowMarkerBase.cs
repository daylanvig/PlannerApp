using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.DayPlannerComponents
{
    public class CalendarNowMarkerBase : ComponentBase
    {
        [Parameter]
        public DateTime ForDate { get; set; }

        protected override bool ShouldRender()
        {   
            var now = DateTime.Now;
            if (ForDate.Hour == now.Hour && ForDate.DayOfWeek == now.DayOfWeek)
            {
                // if looking at this week only
                return ForDate.Date == now.Date;
            }
            return false;
        }

        protected string GetTop() => UIComponentHelper.CalculateTop(DateTime.Now);
    }
}
