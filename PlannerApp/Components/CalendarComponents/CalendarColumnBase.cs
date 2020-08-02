using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarColumnBase : ComponentBase
    {
        [Inject] IPlannerItemService PlannerItemService { get; set; }
        [Inject] IDOMInteropService DOMService { get; set; }
        [Parameter]
        public IEnumerable<PlannerItemDTO> Events { get; set; }
        [Parameter]
        public DateTime Date { get; set; }

        protected ElementReference ColumnEl;
        protected List<PlannerItemDTO> events;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            events = Events.ToList();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            events = Events.ToList();
        }

        protected void UpdateItem(PlannerItemDTO item)
        {
            var existingEvent = events.FirstOrDefault(i => i.ID == item.ID);
            if (existingEvent != null)
            {
                events.Remove(existingEvent);
            }
            events.Add(item);
            StateHasChanged();
        }

        protected void EditItem(PlannerItemDTO item)
        {
            PlannerItemService.ShowAddEditModal(item, UpdateItem);
        }

        protected async Task AddItem(MouseEventArgs e)
        {
            var columnBounds = await DOMService.GetBoundingClientRect(ColumnEl);
            // for every 80px from top, time +=1 hour from midnight
            var clickYRelativeToColumn = e.ClientY - columnBounds.Top;
            var clickedTime = clickYRelativeToColumn / 80;
            var clickedHour = Math.Truncate(clickedTime);
            var partialMinutesClicked = clickedTime - clickedHour;
            // if user clicks near the middle of the hour, use 30 minute as start. Otherwise use start of hour
            var clickedMinute = partialMinutesClicked > .4 ? 30 : 0;
            var startOfInterval = new DateTime(Date.Year, Date.Month, Date.Day, (int)clickedHour, clickedMinute, 0);
            var item = new PlannerItemDTO
            {
                PlannedActionDate = startOfInterval,
                PlannedEndTime = startOfInterval.AddHours(1)
            };
            PlannerItemService.ShowAddEditModal(item, UpdateItem);
        }
    }
}
