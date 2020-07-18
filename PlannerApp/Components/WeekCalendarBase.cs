﻿using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Client.Store.ChangePageUseCase;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Extensions.TouchSwipe;
using UIComponents.Services;

namespace PlannerApp.Client.Components
{
    public class WeekCalendarBase : ComponentBase, ISwipeEventSubscriber
    {
        [Inject]
        public AppState AppState { get; set; }
        [Inject]
        public IDOMInteropService DOMService { get; set; }
        [Inject]
        public IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject]
        public IPlannerItemService PlannerItemService { get; set; }
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Inject]
        public IModalService ModalService { get; set; }
        protected readonly TouchSwipeEvent SwipeEvent = new TouchSwipeEvent();
        public DateTime ViewingWeekOf = DateTimeHelper.GetMostRecentDayOfWeek(DateTime.Today, DayOfWeek.Sunday);
        protected ICollection<PlannerItemDTO> Items = new List<PlannerItemDTO>();

        protected List<DateTime> ViewingDates
        {
            get => GetViewingDates();
        }
      
        protected override async Task OnInitializedAsync()
        {
            SetTitle();
            Items = (await PlannerItemDataService.LoadItems(ViewingWeekOf, ViewingWeekOf.AddDays(7))).ToList();
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

        public void HandleSwipe(SwipeDirection direction)
        {
            if(direction == SwipeDirection.Left)
            {
                ViewingWeekOf = ViewingWeekOf.AddDays(7);
            }
            else
            {
                ViewingWeekOf = ViewingWeekOf.AddDays(-7);
            }
        }

        private void SetTitle()
        {
            AppState.UpdateTitle(
                new TitleState(
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
