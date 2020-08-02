using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components.DashboardComponents
{
    public class OnTimeActionReportBase : ComponentBase
    {
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IDateTimeProvider DateTimeProvider { get; set; }
        protected ICollection<PlannerItemDTO> OverdueItems;

        protected override async Task OnInitializedAsync()
        {
            OverdueItems = (await PlannerItemDataService.LoadOverdueItems()).ToList();
        }

        protected string GetDaysOverdue(PlannerItemDTO item)
        {
            var daysOverdue = DateTimeHelper.DaysBetween(DateTimeProvider.Now, item.PlannedActionDate.Value);
            // less than a day, display hours
            if(daysOverdue < 1)
            {
                return $"{Math.Truncate(daysOverdue * 24)} hours";
            }
            return $"{Math.Truncate(daysOverdue)} days";
        }
    }
}
