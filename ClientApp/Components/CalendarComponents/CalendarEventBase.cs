using Application.PlannerItems.Queries.Common;
using ClientApp.Models;
using ClientApp.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UIComponents.Services;

namespace ClientApp.Components.CalendarComponents
{
    public class CalendarEventBase : ComponentBase
    {
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Inject] IDateTimeGlobalizationService DateTimeGlobalizationService { get; set; }
        [Parameter]
        public PlannerItemModel Item { get; set; }
        [Parameter]
        public EventCallback ClickCallback { get; set; }

        protected string Colour = UIConstants.DEFAULT_CATEGORY_COLOUR;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Item.CategoryID.HasValue)
            {
                Colour = (await CategoryDataService.LoadCategory(Item.CategoryID.Value)).Colour;
            }
            else
            {
                Colour = UIConstants.DEFAULT_CATEGORY_COLOUR;
            }
        }

        private string CalculateHeight() => UIComponentHelper.CalculateHeight(Item.PlannedActionDate, Item.PlannedEndTime);

        private string CalculateTop() => UIComponentHelper.CalculateTop(Item.PlannedActionDate);

        protected string CalculateFontColour()
        {
            return UIComponentHelper.CalculateContrastingFontColour(Colour);
        }

        protected string CalculateCssStyle()
        {
            return $"height: {CalculateHeight()}; top: {CalculateTop()}; background-color: {Colour}";
        }
    }
}
