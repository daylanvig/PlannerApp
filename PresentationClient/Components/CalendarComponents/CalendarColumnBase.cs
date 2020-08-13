using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PresentationClient.Services;
using PresentationClient.Services.ComponentHelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationClient.Components.CalendarComponents
{
    public class CalendarColumnBase : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] IPlannerItemComponentService PlannerItemComponentService { get; set; }
        [Inject] IDOMInteropService DOMService { get; set; }
        [Parameter]
        public IEnumerable<PlannerItemModel> Events { get; set; }
        [Parameter]
        public DateTime Date { get; set; }

        protected ElementReference ColumnEl;
        protected List<PlannerItemModel> events;

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

        protected void UpdateItem(PlannerItemModel item)
        {
            var existingEvent = events.FirstOrDefault(i => i.ID == item.ID);
            if (existingEvent != null)
            {
                events.Remove(existingEvent);
            }
            events.Add(item);
            StateHasChanged();
        }

        protected void EditItem(PlannerItemModel item)
        {
            var editItem = new PlannerItemCreateEditModel
            {
                ID = item.ID,
                Description = item.Description,
                CategoryID = item.CategoryID,
                CompletionDate = item.CompletionDate,
                PlannedActionDate = item.PlannedActionDate,
                PlannedEndTime = item.PlannedEndTime
            };
            PlannerItemComponentService.ShowAddEditModal(Mapper.Map<PlannerItemCreateEditModel>(item), UpdateItem);
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
            var item = new PlannerItemCreateEditModel
            {
                PlannedActionDate = startOfInterval,
                PlannedEndTime = startOfInterval.AddHours(1)
            };
            PlannerItemComponentService.ShowAddEditModal(item, UpdateItem);
        }
    }
}
