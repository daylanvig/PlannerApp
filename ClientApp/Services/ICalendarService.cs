using ClientApp.Models;

namespace ClientApp.Services
{
    public interface ICalendarService
    {
        CalendarState State { get; }
    }
}