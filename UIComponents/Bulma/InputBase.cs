using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIComponents.Bulma
{
    public class InputBase<T> : InputText
    {
        [CascadingParameter] 
        protected FieldBase Field { get; set; }
        [Parameter]
        public string Type { get; set; } = "text";

        public bool IsValid { get; private set; }
        public string ValidationMessage { get; private set; }
        private List<Action> OnChangeLiseners { get; set; } = new List<Action>();

        private string startValue;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            startValue = CurrentValue;
            OnChangeLiseners.Add(() => Field.Notify(this));
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if(CurrentValue != startValue)
            {
                startValue = CurrentValue;
                HandleChange();
            }
        }

        private void NotifyChange()
        {
            OnChangeLiseners.ForEach(action => action.Invoke());
        }

        private void HandleChange()
        {
         //  EditContext.NotifyFieldChanged(FieldIdentifier);
            var errors = EditContext.GetValidationMessages(FieldIdentifier);
            if (errors.Any())
            {
                IsValid = false;
            }
            NotifyChange();
        }
    }
}
