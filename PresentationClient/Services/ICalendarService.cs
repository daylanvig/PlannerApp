using PresentationClient.Models;

namespace PresentationClient.Services
{
    public interface ICalendarService
    {
        CalendarState State { get; }
    }
}