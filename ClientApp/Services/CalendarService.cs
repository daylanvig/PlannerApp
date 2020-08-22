using ClientApp.Models;

namespace ClientApp.Services
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
