using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.DashboardComponents
{
    public class TimeSpentByCategoryReportBase : ComponentBase
    {
        [Inject] ICategoryService CategoryService { get; set; }

        protected IDictionary<CategoryDTO, int> CategoryTimes;
        protected int MaxTime;
        protected override async Task OnInitializedAsync()
        {
            CategoryTimes = await CategoryService.GetTotalMinutesByCategory();
            MaxTime = CategoryTimes.Max(c => c.Value);
        }

        protected string CalculatePercentTotalWidth(int itemTime)
        {
            var relativeToMax = (double)itemTime / MaxTime;
            return $"{relativeToMax * 100}%";
        }
    }
}
