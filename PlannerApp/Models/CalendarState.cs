using System;
using System.Collections;
using System.Collections.Generic;

namespace PlannerApp.Client.Models
{
    public class CalendarState
    {
        public CalendarMode Mode { get; set; } = CalendarMode.Week;
        public DateTime? Date { get; set; }
        public IEnumerable<int?> HiddenCategoryIDs { get; set; } = Array.Empty<int?>();
    }
}
