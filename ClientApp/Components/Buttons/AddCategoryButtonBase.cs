using Application.Categories.Queries.Common;
using ClientApp.Services.ComponentHelperServices;
using Microsoft.AspNetCore.Components;
using System;

namespace ClientApp.Components.Buttons
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
