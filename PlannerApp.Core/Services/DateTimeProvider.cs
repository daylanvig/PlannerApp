using System;

namespace PlannerApp.Shared.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get => DateTime.Now; }
    }
}
