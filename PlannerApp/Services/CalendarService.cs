using PlannerApp.Client.Models;
using PlannerApp.Client.Pages;

namespace PlannerApp.Client.Services
{
    public class CalendarService : ICalendarService
    {
        public CalendarService()
        {
            State = new CalendarState();
        }
        public CalendarState State { get; }
    }
}
