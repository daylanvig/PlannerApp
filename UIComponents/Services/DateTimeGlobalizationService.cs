using System;
using UIComponents.JSInterop.Services;

namespace UIComponents.Services
{
    public class DateTimeGlobalizationService : IDateTimeGlobalizationService
    {
        private readonly IJSDateHelperService jsDateHelperService;
        private TimeSpan? browserOffset;

        public DateTimeGlobalizationService(IJSDateHelperService jsDateHelperService)
        {
            this.jsDateHelperService = jsDateHelperService;
        }

        /// <summary>
        /// Get users current time
        /// </summary>
        /// <value>
        /// Current time (with timezone accounted for)
        /// </value>
        public DateTimeOffset Now { get => ConvertToLocal(DateTimeOffset.Now); }

        /// <summary>
        /// Convert datetimeoffset to local time
        /// </summary>
        /// <remarks>
        /// At this point, blazor always returns the timezone offset as +0. This uses js interop to correctly offset.
        /// </remarks>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public DateTimeOffset ConvertToLocal(DateTimeOffset value)
        {
            if (!browserOffset.HasValue)
            {
                browserOffset = TimeSpan.FromMinutes(jsDateHelperService.GetTimeZoneOffset());
            }
            return value.ToOffset(-1 * browserOffset.Value);
        }
    }
}
