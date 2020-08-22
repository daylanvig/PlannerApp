using Application.Categories.Queries.Common;
using ClientApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ClientApp.Components
{
    public class CategoryFormBase : ComponentBase
    {
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Parameter]
        public CategoryModel Category { get; set; }
        [Parameter]
        public EventCallback<CategoryModel> OnSaveCallback { get; set; }

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
