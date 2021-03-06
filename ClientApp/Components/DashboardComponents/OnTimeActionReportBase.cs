﻿using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using Microsoft.AspNetCore.Components;
using ClientApp.Services;
using ClientApp.Services.ComponentHelperServices;
using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Components.DashboardComponents
{
    public class OnTimeActionReportBase : ComponentBase
    {
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IPlannerItemComponentService PlannerItemComponentService { get; set; }
        [Inject] IDateTimeProvider DateTimeProvider { get; set; }
        protected ICollection<PlannerItemModel> OverdueItems;

        protected override async Task OnInitializedAsync()
        {
            OverdueItems = (await PlannerItemDataService.LoadOverdueItems()).ToList();
        }

        protected void EditItem(PlannerItemModel item)
        {
            var editItem = new PlannerItemCreateEditModel
            {
                CategoryID = item.CategoryID,
                CompletionDate = item.CompletionDate?.LocalDateTime,
                Description = item.Description,
                ID = item.ID,
                PlannedActionDate = item.PlannedActionDate.LocalDateTime,
                PlannedEndTime = item.PlannedEndTime.LocalDateTime
            };
            PlannerItemComponentService.ShowAddEditModal(editItem, (PlannerItemModel savedItem) =>
            {
                OverdueItems.Remove(item);
                if (!savedItem.CompletionDate.HasValue)
                {
                    OverdueItems.Add(savedItem);
                }
                StateHasChanged();
            });
        }

        protected string GetDaysOverdue(PlannerItemModel item)
        {
            var daysOverdue = DateTimeHelper.DaysBetween(DateTimeProvider.Now, item.PlannedActionDate.LocalDateTime);
            // less than a day, display hours
            if(daysOverdue < 1)
            {
                return $"{Math.Truncate(daysOverdue * 24)} hours";
            }
            return $"{Math.Truncate(daysOverdue)} days";
        }
    }
}
