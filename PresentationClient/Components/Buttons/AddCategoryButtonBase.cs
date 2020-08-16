using Application.Categories.Queries.Common;
using Microsoft.AspNetCore.Components;
using PresentationClient.Services.ComponentHelperServices;
using System;

namespace PresentationClient.Components.Buttons
{
    public class AddCategoryButtonBase : ComponentBase
    {
        [Inject] ICategoryComponentService CategoryComponentService { get; set; }
        [Parameter]
        public Action<CategoryModel> OnSave { get; set; }

        protected void BeginAddingItem()
        {
            CategoryComponentService.BeginAddingCategory(OnSave);
        }
    }
}
