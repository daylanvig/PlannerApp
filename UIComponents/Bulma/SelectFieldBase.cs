using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UIComponents.Extensions;

namespace UIComponents.Bulma
{
    public class SelectFieldBase<T> : InputFieldBase<T>
    {
        [Parameter]
        public Func<string, T> StringParser { get; set; }
        [Parameter]
        public Microsoft.AspNetCore.Components.RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            try
            {
                result = StringParser(value);
                validationErrorMessage = string.Empty;
                return true;
            }
            catch
            {
                result = default;
                validationErrorMessage = "Unable to parse value";
                return false;
            }
        }
    }
}
