using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.Client.Components.Buttons
{
    public class AddCategoryButtonBase : ComponentBase
    {
        [Inject] ICategoryService CategoryService { get; set; }
        [Parameter]
        public Action<CategoryDTO> OnSave { get; set; }

        protected void BeginAddingItem()
        {
            CategoryService.BeginAddingCategory(OnSave);
        }
    }
}
