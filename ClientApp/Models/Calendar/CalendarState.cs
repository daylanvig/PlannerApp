using System;
using System.Collections.Generic;

namespace ClientApp.Models
{
    public class CalendarState
    {
        public CalendarMode Mode { get; set; } = CalendarMode.Week;
        public DateTimeOffset? Date { get; set; }
        public IEnumerable<int?> HiddenCategoryIDs { get; set; } = Array.Empty<int?>();
    }
}
