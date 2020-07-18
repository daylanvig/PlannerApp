using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UIComponents.Bulma;
using UIComponents.Bulma.Helpers;
using UIComponents.Services;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarColumnBase : ComponentBase
    {
        [Inject]
        public IModalService ModalService { get; set; }
        [Inject]
        public IDOMInteropService DOMService { get; set; }
        [Parameter]
        public IEnumerable<PlannerItemDTO> Events { get; set; }
        [Parameter]
        public DateTime Date { get; set; }

        protected ElementReference ColumnEl;
        private List<PlannerItemDTO> events;

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
            ModalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }

        protected IEnumerable<PlannerItemDTO> GetItemsByHour(int hour)
        {
            return events.Where(e => e.PlannedActionDate.Value.Hour == hour);
        }

        protected void UpdateItem(PlannerItemDTO item)
        {
            var existingEvent = events.FirstOrDefault(i => i.ID == item.ID);
            if(existingEvent != null)
            {
                events.Remove(existingEvent);
            }
            events.Add(item);
            ModalService.Close();
            StateHasChanged();
        }

        protected void AddItem(MouseEventArgs e, int hour)
        {
            var startOfInterval = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
            var item = new PlannerItemDTO
            {
                PlannedActionDate = startOfInterval,
                PlannedEndTime = startOfInterval.AddHours(1)
            };
            ShowModal(item);
        }
    }
}
