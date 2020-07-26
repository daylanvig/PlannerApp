using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarEventBase : ComponentBase
    {
        [Inject] 
        public ICategoryDataService CategoryDataService { get; set; }
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        [Parameter]
        public EventCallback ClickCallback { get; set; }

        protected string Colour;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Item.CategoryID.HasValue)
            {
                Colour = (await CategoryDataService.LoadCategory(Item.CategoryID.Value)).Colour;
            }
            else
            {
                Colour = "#b2bec3";
            }
        }

        private string CalculateHeight() => UIComponentHelper.CalculateHeight(Item.PlannedActionDate.Value, Item.PlannedEndTime.Value);

        private string CalculateTop() => UIComponentHelper.CalculateTop(Item.PlannedActionDate.Value);

        protected string CalculateFontColour()
        {
            // fallback to treating the background as dark, since default background is grey
            var colour = string.IsNullOrEmpty(Colour) ? "#000000" : Colour;
            return UIComponentHelper.CalculateContrastingFontColour(colour);
        }

        protected string CalculateCssStyle()
        {
            return $"height: {CalculateHeight()}; top: {CalculateTop()}; background-color: {Colour}";
        }
    }
}
