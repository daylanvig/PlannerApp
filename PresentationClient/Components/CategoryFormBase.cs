using Application.Categories.Queries.Common;
using Microsoft.AspNetCore.Components;
using PresentationClient.Services;
using System;
using System.Threading.Tasks;

namespace PresentationClient.Components
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
