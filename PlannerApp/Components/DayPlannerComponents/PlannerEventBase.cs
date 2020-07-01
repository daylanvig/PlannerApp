using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;

namespace PlannerApp.Client.Components.DayPlannerComponents
{
    public class PlannerEventBase : ComponentBase
    {
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        [Parameter]
        public EventCallback ClickCallback { get; set; }

        protected string CalculateHeight() => CalendarComponentHelper.CalculateHeight(Item.PlannedActionDate.Value, Item.PlannedEndTime.Value);

        protected string CalculateTop() => CalendarComponentHelper.CalculateTop(Item.PlannedActionDate.Value);
    }
}
