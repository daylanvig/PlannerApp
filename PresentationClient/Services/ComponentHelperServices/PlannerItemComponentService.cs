using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using Microsoft.AspNetCore.Components;
using PresentationClient.Components;
using System;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PresentationClient.Services.ComponentHelperServices
{
    public class PlannerItemComponentService : IPlannerItemComponentService
    {
        readonly IApplicationWideComponentService<ModalParams> modalService;
        private Action<PlannerItemModel> onSaveCallback;

        public PlannerItemComponentService(IApplicationWideComponentService<ModalParams> modalService)
        {
            this.modalService = modalService;
        }

        private void CloseAddEditModal(PlannerItemModel item)
        {
            if (onSaveCallback != null)
            {
                onSaveCallback.Invoke(item);
                onSaveCallback = null;
            }

            modalService.Close();
        }

        public void ShowAddEditModal(PlannerItemCreateEditModel item, Action<PlannerItemModel> onSave)
        {
            onSaveCallback = onSave;
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "aside");
                builder.AddAttribute(0, "class", "box");
                builder.AddAttribute(1, "style", "overflow-y: auto");
                builder.OpenComponent<PlannerItemForm>(1);
                builder.AddAttribute(1, "Item", item);
                builder.AddAttribute(2, "OnItemSaveCallback", EventCallback.Factory.Create<PlannerItemModel>(this, CloseAddEditModal));
                builder.CloseComponent();
                builder.CloseElement();
            });
            modalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal, modalClass: "is-fullscreen-mobile"));
        }
    }
}
