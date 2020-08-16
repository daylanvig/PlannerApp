using Application.Categories.Queries.Common;
using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using Microsoft.AspNetCore.Components;
using PresentationClient.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationClient.Components
{
    public class PlannerItemFormBase : ComponentBase
    {
        [Inject]
        public IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Parameter]
        public PlannerItemCreateEditModel Item { get; set; }
        [Parameter]
        public EventCallback<PlannerItemModel> OnItemSaveCallback { get; set; }

        protected IEnumerable<CategoryModel> Categories = new List<CategoryModel>();

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryDataService.LoadCategories();
        }

        protected async Task SavePlannerItem()
        {
            PlannerItemModel newItem;
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

        protected async Task MarkComplete()
        {
            Item.CompletionDate = DateTime.Now;
            // don't invoke SavePlannerItem since it closes the modal.
            // this function is only available when editing, so no need to handle create
            await PlannerItemDataService.EditItem(Item);
        }
    }
}
