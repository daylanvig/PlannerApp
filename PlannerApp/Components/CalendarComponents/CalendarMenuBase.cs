using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Models;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Bulma;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarMenuBase : ComponentBase, IDisposable
    {
        [Inject] ICategoryDataService CategoryDataService { get; set; }
        [Inject] 
        protected ICalendarService CalendarService { get; set; }
        [Parameter]
        public EventCallback OnStateChange { get; set; }
        protected SelectField<CalendarMode> ModeInput;
        protected DateField DateInput;
        protected IList<Filter<CategoryDTO>> CategoryFilters;
        protected Filter<int?> NoCategoryFilter;
        private Action callStateChange;

        protected override async Task OnInitializedAsync()
        {
            callStateChange = async () => await OnStateChange.InvokeAsync(null);
            var categories = await CategoryDataService.LoadCategories();
            CategoryFilters = categories.Select(c => new Filter<CategoryDTO>
            {
                Model = c,
                IsVisible = !CalendarService.State.HiddenCategoryIDs.Contains(c.ID)
            }).ToList();
            NoCategoryFilter = new Filter<int?>
            {
                IsVisible = !CalendarService.State.HiddenCategoryIDs.Contains(null),
                Model = null
            };
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            ModeInput.Subscribe(callStateChange);
            DateInput.Subscribe(callStateChange);
        }

        protected void HandleCategoryAdded(CategoryDTO category)
        {
            CategoryFilters.Add(new Filter<CategoryDTO>
            {
                IsVisible = true,
                Model = category
            });
            StateHasChanged();
        }

        protected void UpdateCategoryFilters(int? changedCategoryID)
        {
            if (changedCategoryID.HasValue)
            {
                var changedFilter = CategoryFilters.First(cf => cf.Model.ID == changedCategoryID);
                changedFilter.IsVisible = !changedFilter.IsVisible;
            }
            else
            {
                NoCategoryFilter.IsVisible = !NoCategoryFilter.IsVisible;
            }
            List<int?> hiddenFilters = CategoryFilters
                                        .Where(cf => !cf.IsVisible)
                                        .Select(cf => (int?)cf.Model.ID)
                                        .ToList();
            if (!NoCategoryFilter.IsVisible)
            {
                hiddenFilters.Append(null);
            }
            CalendarService.State.HiddenCategoryIDs = hiddenFilters;
            callStateChange();
        }

        public void Dispose()
        {
            ModeInput.Unsubscribe(callStateChange);
            DateInput.Unsubscribe(callStateChange);
        }
    }
}
