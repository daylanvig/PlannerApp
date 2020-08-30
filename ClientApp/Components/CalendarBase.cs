using Application.PlannerItems.Queries.Common;
using ClientApp.Models;
using ClientApp.Services;
using ClientApp.Store.ChangePageUseCase;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Custom.SheetComponent;
using UIComponents.Extensions.TouchSwipe;
using UIComponents.JSInterop.Services;

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
            var items = await PlannerItemDataService.LoadItems(CalendarService.State.Date.Value.LocalDateTime, CalendarService.State.Date.Value.LocalDateTime.AddDays(GetDaysVisible() - 1));
            Items = items.Where(item => !CalendarService.State.HiddenCategoryIDs.Contains(item.CategoryID)).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            CalendarService.State.Date = DateTimeHelper.GetMostRecentDayOfWeek(DateTimeProvider.NowLocal, DayOfWeek.Sunday);
            SetTitle();
            await LoadData();
            SwipeEvent.Subscribe(this);
        }

        
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && DateTime.Now.Hour != 0)
            {
                // display current time
                DOMService.ScrollIntoView($"#interval-{DateTime.Now.Hour - 1}");
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
                await GoToNextWeek();
            }
            else if (direction == SwipeDirection.Right)
            {
                await GoToLastWeek();
            }
            // ignore up and down
        }

        protected async Task HandleKeyPress(KeyboardEventArgs e)
        {
            Console.WriteLine(e.Code);
            Console.WriteLine(e.Key);
        }

        private async Task GoToNextWeek()
        {
            CalendarService.State.Date = CalendarService.State.Date.Value.AddDays(GetDaysVisible());
            await HandleStateChange();
        }

        private async Task GoToLastWeek()
        {
            CalendarService.State.Date = CalendarService.State.Date.Value.AddDays(-1 * GetDaysVisible());
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
                dates.Add(CalendarService.State.Date.Value.LocalDateTime.AddDays(i));
            }
            return dates;
        }

    }
}
