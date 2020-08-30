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

        public DateTimeOffset ConvertToLocal(DateTimeOffset value)
        {
            if (!browserOffset.HasValue)
            {
                browserOffset = TimeSpan.FromMinutes(jsDateHelperService.GetTimeZoneOffset());
            }
            return value.ToOffset(browserOffset.Value);
        }
    }
}
