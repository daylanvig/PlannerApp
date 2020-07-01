using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public class WeekCalendarBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject]
        public IPlannerItemService PlannerItemService { get; set; }
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
      
        public DateTime ViewingWeekOf { get; set; } = DateTimeHelper.GetMostRecentDayOfWeek(DateTime.Today, DayOfWeek.Sunday);
        protected ICollection<PlannerItemDTO> Items;

        private IEnumerable<CategoryDTO> categories;

        private async Task LoadItems()
        {
            Items = (await PlannerItemDataService.LoadItems(ViewingWeekOf, ViewingWeekOf.AddDays(7))).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            categories = await CategoryDataService.LoadCategories();
            await LoadItems();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && DateTime.Now.Hour != 0)
            {
                // scroll to display current time
                await JSRuntime.InvokeVoidAsync("customScripts.scrollIntoView", $"#interval-{DateTime.Now.Hour - 1}");
            }
        }

        protected DateTime GetDateForHourInterval(int daysFromSunday, int hour)
        {
            var date = ViewingWeekOf.AddDays(daysFromSunday); // starts at midnight on sunday
            return date.AddHours(hour);
        }

        protected string GetCategoryColour(PlannerItemDTO item)
        {
            if (item.CategoryID.HasValue) {
                return categories.First(c => c.ID == item.CategoryID).Colour;
            }
            return "";
        }
    }
}
