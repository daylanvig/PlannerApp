using Microsoft.AspNetCore.Components;
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
    public class CategoryService
    {
        [Inject] IApplicationWideComponentService<ModalParams> ModalService { get; set; }

        public void BeginAddingCategory()
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenComponent<CategoryForm>(0);
                builder.AddAttribute(1, "Category", new CategoryDTO());
                builder.AddAttribute(2, "OnSaveCallback", EventCallback.Factory.Create<CategoryDTO>(this, ModalService.Close));
                builder.CloseComponent();
            });
            ModalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }
    }
}
