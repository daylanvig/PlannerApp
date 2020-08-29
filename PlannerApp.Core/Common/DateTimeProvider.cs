using System;

namespace PlannerApp.Shared.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get => DateTime.Now; }
        public DateTime NowLocal { get => DateTimeOffset.Now.LocalDateTime;  }
    }
}
