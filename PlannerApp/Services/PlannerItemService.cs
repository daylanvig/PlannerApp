using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PlannerApp.Client.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client.Services
{
    public class PlannerItemService : IPlannerItemService
    {
        readonly IApplicationWideComponentService<ModalParams> modalService;
        private Action<PlannerItemDTO> onSaveCallback;

        public PlannerItemService(IApplicationWideComponentService<ModalParams> modalService)
        {
            this.modalService = modalService;
        }

        private void CloseAddEditModal(PlannerItemDTO item)
        {
            if(onSaveCallback != null)
            {
                onSaveCallback.Invoke(item);
                onSaveCallback = null;
            }
            
            modalService.Close();
        }

        public void ShowAddEditModal(PlannerItemDTO item, Action<PlannerItemDTO> onSave)
        {
            onSaveCallback = onSave;
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "aside");
                builder.AddAttribute(0, "class", "box");
                builder.AddAttribute(1, "style", "overflow-y: auto"); // todo move this elsewhere once done testing
                builder.OpenComponent<PlannerItemForm>(1);
                builder.AddAttribute(1, "Item", item);
                builder.AddAttribute(2, "OnItemSaveCallback", EventCallback.Factory.Create<PlannerItemDTO>(this, CloseAddEditModal));
                builder.CloseComponent();
                builder.CloseElement();
            });
            modalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal, modalClass: "is-fullscreen-mobile"));
        }
    }
}
