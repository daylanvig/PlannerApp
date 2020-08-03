using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Components;
using PlannerApp.Shared.Models;
using System;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client.Services.ComponentHelperServices
{
    public class PlannerItemComponentService : IPlannerItemComponentService
    {
        readonly IApplicationWideComponentService<ModalParams> modalService;
        private Action<PlannerItemDTO> onSaveCallback;

        public PlannerItemComponentService(IApplicationWideComponentService<ModalParams> modalService)
        {
            this.modalService = modalService;
        }

        private void CloseAddEditModal(PlannerItemDTO item)
        {
            if (onSaveCallback != null)
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
                builder.AddAttribute(1, "style", "overflow-y: auto");
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
