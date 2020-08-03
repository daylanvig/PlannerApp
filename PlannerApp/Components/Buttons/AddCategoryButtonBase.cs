using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.ComponentHelperServices;
using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.Client.Components.Buttons
{
    public class AddCategoryButtonBase : ComponentBase
    {
        [Inject] ICategoryComponentService CategoryComponentService { get; set; }
        [Parameter]
        public Action<CategoryDTO> OnSave { get; set; }

        protected void BeginAddingItem()
        {
            CategoryComponentService.BeginAddingCategory(OnSave);
        }
    }
}
