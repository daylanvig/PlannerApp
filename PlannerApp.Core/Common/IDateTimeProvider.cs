using System;

namespace PlannerApp.Shared.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime NowLocal { get; }
    }
}