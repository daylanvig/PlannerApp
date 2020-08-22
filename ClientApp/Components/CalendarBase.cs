using Application.PlannerItems.Queries.Common;
using ClientApp.Models;
using ClientApp.Services;
using ClientApp.Store.ChangePageUseCase;
using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Custom.SheetComponent;
using UIComponents.Extensions.TouchSwipe;

namespace ClientApp.Components
{
    public class CalendarBase : ComponentBase, ISwipeEventSubscriber
    {
        [Inject] IAppState AppState { get; set; }
        [Inject] IDOMInteropService DOMService { get; set; }
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IDateTimeProvider DateTimeProvider { get; set; }
        [Inject] ICalendarService CalendarService { get; set; }
        protected readonly TouchSwipeEvent SwipeEvent = new TouchSwipeEvent();
        protected ICollection<PlannerItemModel> Items = new List<PlannerItemModel>();

        protected List<DateTime> ViewingDates
        {
            get => GetViewingDates();
        }

        private int GetDaysVisible()
        {
            return CalendarService.State.Mode switch
            {
                CalendarMode.Day => 1,
                CalendarMode.Week => 7,
                _ => DateTime.DaysInMonth(CalendarService.State.Date.Value.Year, CalendarService.State.Date.Value.Month),
            };
        }
        private async Task LoadData()
        {
            var items = await PlannerItemDataService.LoadItems(CalendarService.State.Date, CalendarService.State.Date.Value.AddDays(GetDaysVisible() - 1));
            Items = items.Where(item => !CalendarService.State.HiddenCategoryIDs.Contains(item.CategoryID)).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            CalendarService.State.Date = DateTimeHelper.GetMostRecentDayOfWeek(DateTimeProvider.Now, DayOfWeek.Sunday);
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
        public async Task HandleSwipe(SwipeDirection direction)
        {
            if (direction == SwipeDirection.Left)
            {
                CalendarService.State.Date = CalendarService.State.Date.Value.AddDays(GetDaysVisible());
            }
            else if (direction == SwipeDirection.Right)
            {
                CalendarService.State.Date = CalendarService.State.Date.Value.AddDays(-1 * GetDaysVisible());
            }
            // ignore up and down
            await HandleStateChange();
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
                f.OpenComponent<CalendarComponents.CalendarMenu>(1);
                f.AddAttribute(2, nameof(CalendarComponents.CalendarMenu.OnStateChange), EventCallback.Factory.Create(this, HandleStateChange));
                f.CloseComponent();
            });
            AppState.UpdateTitle(
                new NavMenuState(
                    $"<span class='has-text-weight-light has-padding-right-5'>Calendar</span><span class='has-text-weight-semibold'>{CalendarService.State.Date:yyyy}</span>",
                    $"<span class='has-text-weight-semibold'>{CalendarService.State.Date:MMM}</span>",
                    sheetParams: new SheetParams { Body = fragment }
                ));
        }

        private List<DateTime> GetViewingDates()
        {
            var dates = new List<DateTime>();
            for (var i = 0; i < GetDaysVisible(); i++)
            {
                dates.Add(CalendarService.State.Date.Value.AddDays(i));
            }
            return dates;
        }

    }
}
