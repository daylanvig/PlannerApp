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
        [Inject]
        public IApplicationWideComponentService<ModalParams> ModalService { get; set; }
        [Inject]
        public IDOMInteropService DOMService { get; set; }
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

        protected void ShowModal(PlannerItemDTO item)
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "aside");
                builder.AddAttribute(0, "class", "box");
                builder.AddAttribute(1, "style", "overflow-y: auto"); // todo move this elsewhere once done testing
                builder.OpenComponent<PlannerItemForm>(1);
                builder.AddAttribute(1, "Item", item);
                builder.AddAttribute(2, "OnItemSaveCallback", EventCallback.Factory.Create<PlannerItemDTO>(this, UpdateItem));
                builder.CloseComponent();
                builder.CloseElement();
            });
            ModalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal, modalClass: "is-fullscreen-mobile"));
        }

        protected void UpdateItem(PlannerItemDTO item)
        {
            var existingEvent = events.FirstOrDefault(i => i.ID == item.ID);
            if (existingEvent != null)
            {
                events.Remove(existingEvent);
            }
            events.Add(item);
            ModalService.Close();
            StateHasChanged();
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
            ShowModal(item);
        }
    }
}
