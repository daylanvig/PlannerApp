using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.DayPlannerComponents
{
    public class DayPlannerEntryBase : ComponentBase
    {
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        [Parameter]
        public EventCallback ClickCallback { get; set; }

        private const int ROWHEIGHT = 78; // px


        protected string CalculateHeight()
        {
            var fractionsOfHour = DateTimeHelper.CalculateLength(Item.PlannedActionDate.Value, Item.PlannedEndTime.Value) / 60;
            // TODO: move constant out somewhere possibly
            // parents are 78px high
            return $"{ROWHEIGHT * fractionsOfHour}px";
        }


        protected string CalculateTop()
        {
            var startMinuteFractions = (double)Item.PlannedActionDate.Value.Minute / 60;
            Console.WriteLine(((double)Item.PlannedActionDate.Value.Minute) / 60);
            return $"{ROWHEIGHT * startMinuteFractions}px";
        }
    }
}
