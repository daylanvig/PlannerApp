using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using PlannerApp.Client.Components.CalendarComponents;
using PlannerApp.Client.Models;
using PlannerApp.Client.Services;
using PlannerApp.Client.Store.ChangePageUseCase;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Custom.SheetComponent;
using UIComponents.Extensions.TouchSwipe;

namespace PlannerApp.Client.Components
{
    public class CalendarBase : ComponentBase, ISwipeEventSubscriber
    {
        [Inject] IAppState AppState { get; set; }
        [Inject] IDOMInteropService DOMService { get; set; }
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IDateTimeProvider DateTimeProvider { get; set; }

        protected readonly TouchSwipeEvent SwipeEvent = new TouchSwipeEvent();
        private CalendarState state;
        protected ICollection<PlannerItemDTO> Items = new List<PlannerItemDTO>();

        protected List<DateTime> ViewingDates
        {
            get => GetViewingDates();
        }

        private int GetDaysVisible()
        {
            switch (state.Mode)
            {
                case CalendarMode.Day:
                    return 1;
                case CalendarMode.Week:
                    return 7;
                default:
                    return DateTime.DaysInMonth(state.Date.Value.Year, state.Date.Value.Month);
            }
            // if fell through 
        }
        private async Task LoadData()
        {
            Items = (await PlannerItemDataService.LoadItems(state.Date, state.Date.Value.AddDays(GetDaysVisible() - 1))).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            state = new CalendarState
            {
                Date = DateTimeHelper.GetMostRecentDayOfWeek(DateTimeProvider.Now, DayOfWeek.Sunday)
            };
            SetTitle();
            await LoadData();
            SwipeEvent.Subscribe(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && DateTime.Now.Hour != 0)
            {
                // display current time
                await DOMService.ScrollIntoView($"#interval-{DateTime.Now.Hour - 1}");
            }
        }

        /// <summary>
        /// Handle changing date based on user swiping page.
        /// Allows user to move forwards/backwards by one week.
        /// </summary>
        /// <param name="direction"></param>
        public void HandleSwipe(SwipeDirection direction)
        {
            if (direction == SwipeDirection.Left)
            {
                state.Date = state.Date.Value.AddDays(GetDaysVisible());
            }
            else if (direction == SwipeDirection.Right)
            {
                state.Date = state.Date.Value.AddDays(GetDaysVisible());
            }
            // ignore up and down
        }

        private async Task HandleStateChange()
        {
            await LoadData();
            StateHasChanged();
        }

        private void SetTitle()
        {
            var fragment = new RenderFragment(f =>
            {
                f.OpenComponent<CalendarMenu>(1);
                f.AddAttribute(1, nameof(CalendarMenu.State), state);
                f.AddAttribute(2, nameof(CalendarMenu.OnStateChange), EventCallback.Factory.Create(this, HandleStateChange));
                f.CloseComponent();
            });
            AppState.UpdateTitle(
                new NavMenuState(
                    $"<span class='has-text-weight-light has-padding-right-5'>Calendar</span><span class='has-text-weight-semibold'>{state.Date:yyyy}</span>",
                    $"<span class='has-text-weight-semibold'>{state.Date:MMM}</span>",
                    sheetParams: new SheetParams { Body = fragment }
                ));
        }

        private List<DateTime> GetViewingDates()
        {
            var dates = new List<DateTime>();
            for (var i = 0; i < GetDaysVisible(); i++)
            {
                dates.Add(state.Date.Value.AddDays(i));
            }
            return dates;
        }

    }
}
