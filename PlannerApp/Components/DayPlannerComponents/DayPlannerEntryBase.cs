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

        // TODO: move constant out somewhere possibly
        private const int ROWHEIGHT = 78; // px

        /// <summary>
        /// Calculate height in px
        /// </summary>
        /// <returns>px value for css height</returns>
        protected string CalculateHeight()
        {
            var fractionsOfHour = DateTimeHelper.CalculateLength(Item.PlannedActionDate.Value, Item.PlannedEndTime.Value) / 60;
            
            return $"{ROWHEIGHT * fractionsOfHour}px";
        }

        /// <summary>
        /// Get position to set top
        /// </summary>
        /// <returns>px value</returns>
        protected string CalculateTop()
        {
            var startMinuteFractions = (double)Item.PlannedActionDate.Value.Minute / 60;
            return $"{ROWHEIGHT * startMinuteFractions}px";
        }
    }
}
