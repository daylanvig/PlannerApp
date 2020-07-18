using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlannerApp.Client.Services
{
    public class PlannerItemService : IPlannerItemService
    {
        public IEnumerable<PlannerItemDTO> FilterForInterval(IEnumerable<PlannerItemDTO> items, int startHour, DayOfWeek? dayOfWeek = null)
        {
            var itemsInInterval = items.Where(i => i.PlannedActionDate.Value.Hour == startHour);
            if (dayOfWeek.HasValue)
            {
                itemsInInterval = itemsInInterval.Where(i => i.PlannedActionDate.Value.DayOfWeek == dayOfWeek.Value);
            }
            return itemsInInterval;
        }
    }
}
