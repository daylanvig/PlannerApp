using System;

namespace PlannerApp.Server.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}