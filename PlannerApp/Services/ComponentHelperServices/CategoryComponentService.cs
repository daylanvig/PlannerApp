using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Components;
using PlannerApp.Shared.Models;
using System;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client.Services.ComponentHelperServices
{
    public class CategoryComponentService : ICategoryComponentService
    {
        readonly IApplicationWideComponentService<ModalParams> modalService;
        public CategoryComponentService(IApplicationWideComponentService<ModalParams> modalService)
        {
            this.modalService = modalService;
        }

        public void BeginAddingCategory(Action<CategoryDTO> onSave)
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenComponent<CategoryForm>(0);
                builder.AddAttribute(1, "Category", new CategoryDTO());
                builder.AddAttribute(2, "OnSaveCallback", EventCallback.Factory.Create<CategoryDTO>(this, (CategoryDTO savedItem) =>
                {
                    modalService.Close();
                    onSave?.Invoke(savedItem);
                }));
                builder.CloseComponent();
            });
            modalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }
    }
}
