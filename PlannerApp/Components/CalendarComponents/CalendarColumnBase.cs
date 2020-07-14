using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
        [Parameter]
        public IEnumerable<PlannerItemDTO> Events { get; set; }
        [Parameter]
        public DateTime Date { get; set; }
        
        protected void ShowModal(PlannerItemForm item)
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
            return Events.Where(e => e.PlannedActionDate.Value.Hour == hour);
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

        protected void UpdateItem(PlannerItemDTO item)
        {
            // todo
        }

        protected void AddItem(MouseEventArgs e, int hour)
        {
            Console.WriteLine(e.ScreenY);
            Console.WriteLine(e.ClientY);
            Console.WriteLine(JsonSerializer.Serialize(e));
        }
    }
}
