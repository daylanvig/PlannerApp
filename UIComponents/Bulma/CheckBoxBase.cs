using System;
using System.Collections.Generic;
using System.Text;

namespace UIComponents.Bulma
{
    public class CheckBoxBase : InputFieldBase<bool>
    {
        protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = false;
                validationErrorMessage = string.Empty;
                return true;
            }
            if(bool.TryParse(value, out bool parsedValue))
            {
                result = parsedValue;
                validationErrorMessage = string.Empty;
                return true;
            }
            validationErrorMessage = "invalid format";
            result = false;
            return false;
        }
    }
}
