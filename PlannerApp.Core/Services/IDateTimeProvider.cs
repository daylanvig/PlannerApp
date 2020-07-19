using System;

namespace PlannerApp.Shared.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}