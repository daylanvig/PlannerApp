using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.Client.Components.DayPlannerComponents
{
    public class PlannerEventBase : ComponentBase
    {
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        [Parameter]
        public string Colour { get; set; } = "";
        [Parameter]
        public EventCallback ClickCallback { get; set; }

        protected string CalculateHeight() => UIComponentHelper.CalculateHeight(Item.PlannedActionDate.Value, Item.PlannedEndTime.Value);

        protected string CalculateTop() => UIComponentHelper.CalculateTop(Item.PlannedActionDate.Value);

        protected string CalculateFontColour()
        {
            // fallback to treating the background as dark, since default background is grey
            var colour = string.IsNullOrEmpty(Colour) ? "#000000" : Colour;
            return UIComponentHelper.CalculateContrastingFontColour(colour);
        }
    }
}
