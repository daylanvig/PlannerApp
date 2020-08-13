using PresentationClient.Models;
using PresentationClient.Pages;

namespace PresentationClient.Services
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
