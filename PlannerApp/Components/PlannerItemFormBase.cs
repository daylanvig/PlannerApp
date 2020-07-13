using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public class PlannerItemFormBase : ComponentBase
    {
        [Inject]
        public IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        [Parameter]
        public EventCallback<PlannerItemDTO> OnItemSaveCallback { get; set; }

        protected IEnumerable<CategoryDTO> Categories = new List<CategoryDTO>();

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryDataService.LoadCategories();
        }

        protected async Task SavePlannerItem()
        {
            PlannerItemDTO newItem;
            // new item defaults to 0
            if (Item.ID == 0)
            {
                newItem = await PlannerItemDataService.AddItem(Item);
            }
            else
            {
                newItem = await PlannerItemDataService.EditItem(Item);
            }
            await OnItemSaveCallback.InvokeAsync(newItem);
        }
    }
}
