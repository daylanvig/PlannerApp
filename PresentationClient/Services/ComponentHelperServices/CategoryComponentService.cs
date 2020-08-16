using Application.Categories.Queries.Common;
using Microsoft.AspNetCore.Components;
using PresentationClient.Components;
using System;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PresentationClient.Services.ComponentHelperServices
{
    public class CategoryComponentService : ICategoryComponentService
    {
        readonly IApplicationWideComponentService<ModalParams> modalService;
        public CategoryComponentService(IApplicationWideComponentService<ModalParams> modalService)
        {
            this.modalService = modalService;
        }

        public void BeginAddingCategory(Action<CategoryModel> onSave)
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenComponent<CategoryForm>(0);
                builder.AddAttribute(1, "Category", new CategoryModel());
                builder.AddAttribute(2, "OnSaveCallback", EventCallback.Factory.Create<CategoryModel>(this, (CategoryModel savedItem) =>
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
