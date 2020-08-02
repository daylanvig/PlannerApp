using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client.Components.DashboardComponents
{
    public class TimeSpentByCategoryReportBase : ComponentBase
    {
        [Inject] ICategoryService CategoryService { get; set; }
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IApplicationWideComponentService<ModalParams> ModalService { get; set; }

        protected IDictionary<CategoryDTO, int> CategoryTimes;
        protected int MaxTime;
        protected override async Task OnInitializedAsync()
        {
            CategoryTimes = await CategoryService.GetTotalMinutesByCategory();
            MaxTime = CategoryTimes.Any() ? CategoryTimes.Max(c => c.Value) : 0;
        }

        protected string CalculatePercentTotalWidth(int itemTime)
        {
            if (MaxTime == 0)
            {
                return "0%";
            }
            var relativeToMax = (double)itemTime / MaxTime;
            return $"{relativeToMax * 100}%";
        }

        protected async Task ShowBreakDown(CategoryDTO category)
        {
            var items = await PlannerItemDataService.LoadCompletedItemsByCategoryID(category.ID);
            var body = new RenderFragment(builder =>
           {
               builder.OpenComponent<EventList>(0);
               builder.AddAttribute(1, nameof(EventListBase.Items), items);
               builder.CloseComponent();
           });
            ModalService.Show(new ModalParams(body, style: ModalStyle.Normal, saveLabel: string.Empty));
        }
    }
}
