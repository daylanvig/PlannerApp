using PlannerApp.Client.Models;

namespace PlannerApp.Client.Services
{
    public interface ICalendarService
    {
        CalendarState State { get; }
    }
}