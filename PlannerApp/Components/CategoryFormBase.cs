using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public class CategoryFormBase : ComponentBase
    {
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Parameter]
        public CategoryDTO Category { get; set; }
        [Parameter]
        public EventCallback<CategoryDTO> OnSaveCallback { get; set; }

        protected async Task SaveForm()
        {
            var createdCategory = await CategoryDataService.AddItem(Category);
            if(createdCategory == null)
            {
                throw new ArgumentException("Could not save category");
            }
            await OnSaveCallback.InvokeAsync(createdCategory);
        }
    }
}
