using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;

namespace PlannerApp.Client.Services
{
    public interface IPlannerItemService
    {
        IEnumerable<PlannerItemDTO> FilterForInterval(IEnumerable<PlannerItemDTO> items, int startHour, DayOfWeek? dayOfWeek = null);
    }
}