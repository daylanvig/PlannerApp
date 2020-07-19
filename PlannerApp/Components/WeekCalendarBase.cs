using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Client.Store.ChangePageUseCase;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Bulma.Modal;
using UIComponents.Extensions.TouchSwipe;
using UIComponents.Services;

namespace PlannerApp.Client.Components
{
    public class WeekCalendarBase : ComponentBase, ISwipeEventSubscriber
    {
        [Inject] IAppState AppState { get; set; }
        [Inject] IDOMInteropService DOMService { get; set; }
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IDateTimeProvider DateTimeProvider { get; set; }
        protected readonly TouchSwipeEvent SwipeEvent = new TouchSwipeEvent();
        private DateTime? viewingWeekOf;
        public DateTime ViewingWeekOf
        {
            get {
                return viewingWeekOf ?? DateTimeHelper.GetMostRecentDayOfWeek(DateTimeProvider.Now, DayOfWeek.Sunday);
            }
            set
            {
                viewingWeekOf = value;
            }
        }
        protected ICollection<PlannerItemDTO> Items = new List<PlannerItemDTO>();

        protected List<DateTime> ViewingDates
        {
            get => GetViewingDates();
        }
      
        protected override async Task OnInitializedAsync()
        {
            SetTitle();
            Items = (await PlannerItemDataService.LoadItems(ViewingWeekOf, ViewingWeekOf.AddDays(6))).ToList();
            SwipeEvent.Subscribe(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && DateTime.Now.Hour != 0)
            {
                // scroll to display current time
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
            if(direction == SwipeDirection.Left)
            {
                ViewingWeekOf = ViewingWeekOf.AddDays(7);
            }
            else if (direction == SwipeDirection.Right)
            {
                ViewingWeekOf = ViewingWeekOf.AddDays(-7);
            }
            // ignore up and down
        }

        private void SetTitle()
        {
            AppState.UpdateTitle(
                new NavMenuState(
                    $"<span class='has-text-weight-light has-padding-right-5'>Calendar</span><span class='has-text-weight-semibold'>{ViewingWeekOf:yyyy}</span>",
                    $"<span class='has-text-weight-semibold'>{ViewingWeekOf:MMM}</span>"
                ));
        }

        private List<DateTime> GetViewingDates()
        {
            var dates = new List<DateTime>();
            for (var i = 0; i < 7; i++)
            {
                dates.Add(ViewingWeekOf.AddDays(i));
            }
            return dates;
        }

    }
}
