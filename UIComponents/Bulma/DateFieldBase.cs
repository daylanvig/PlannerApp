using Microsoft.AspNetCore.Components;
using System;

namespace UIComponents.Bulma
{
    public class DateFieldBase : InputFieldBase<DateTime?>
    {
        [Parameter]
        public bool HasTime { get; set; }
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
        protected override string FormatValueAsString(DateTime? value)
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
                    return value.Value.ToString("yyyy-MM-ddTHH:mm");
                }
                else
                {
                    throw new ArgumentException(nameof(Type), "Invalid date field type");
                }
            }
            return string.Empty;
        }

        protected override bool TryParseValueFromString(string value, out DateTime? result, out string validationErrorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = null;
                validationErrorMessage = string.Empty;
            }
            else if (DateTime.TryParse(value, out var parsedResult))
            {
                result = new DateTime(parsedResult.Ticks, DateTimeKind.Local);
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
