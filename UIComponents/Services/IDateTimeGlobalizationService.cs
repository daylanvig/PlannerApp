using System;

namespace UIComponents.Services
{
    public interface IDateTimeGlobalizationService
    {
        DateTimeOffset ConvertToLocal(DateTimeOffset value);
        DateTimeOffset Now { get; }
    }
}