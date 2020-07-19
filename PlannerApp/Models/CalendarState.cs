using System;

namespace PlannerApp.Client.Models
{
    public class CalendarState
    {
        public CalendarMode Mode { get; set; } = CalendarMode.Week;
        public DateTime? Date { get; set; }
    }
}
