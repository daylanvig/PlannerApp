using Microsoft.AspNetCore.Components;
using PresentationClient.Services.ComponentHelperServices;
using PlannerApp.Shared.Models;
using System;

namespace PresentationClient.Components.Buttons
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
