using Microsoft.AspNetCore.Components;
using System;
using UIComponents.Services;

namespace UIComponents.Bulma
{
    public class DateFieldBase : InputFieldBase<DateTimeOffset?>
    {
        [Parameter]
        public bool HasTime { get; set; }
        [Inject] IDateTimeGlobalizationService DateTimeGlobalizationService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (HasTime)
            {
                Type = "datetime-local";
            }
            else
            {
                Type = "date";
            }
        }

        protected override string FormatValueAsString(DateTimeOffset? value)
        {

            if (value.HasValue)
            {
                if (Type == "date")
                {
                    return value.Value.ToString("yyyy-MM-dd");
                }
                else if (Type == "datetime-local")
                {
                    // html expected format
                    return value.Value.LocalDateTime.ToString("yyyy-MM-ddTHH:mm");
                }
                else
                {
                    throw new ArgumentException(nameof(Type), "Invalid date field type");
                }
            }
            return string.Empty;
        }

        protected override bool TryParseValueFromString(string value, out DateTimeOffset? result, out string validationErrorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = null;
                validationErrorMessage = string.Empty;
            }
            else if (DateTime.TryParse(value, out var parsedResult))
            {
                Console.WriteLine(DateTimeGlobalizationService.ConvertToLocal(parsedResult));
                result = DateTimeGlobalizationService.ConvertToLocal(parsedResult);
                Console.WriteLine(result.Value.ToUniversalTime());
                validationErrorMessage = string.Empty;
            }
            else
            {
                result = null;
                validationErrorMessage = "Unable to parse date format";
                return false;
            }
            return true;
        }
    }
}
