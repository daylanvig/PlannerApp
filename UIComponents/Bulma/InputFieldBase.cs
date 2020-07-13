using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIComponents.Bulma
{

    public abstract class InputFieldBase<T> : Microsoft.AspNetCore.Components.Forms.InputBase<T>
    {
        private T value;
        [CascadingParameter] 
        protected FieldBase Field { get; set; }
        [Parameter]
        public virtual string Type { get; set; } = "text";
        [Parameter]
        public string ControlClass { get; set; }
        public bool IsValid { get; private set; }
        public string ValidationMessage { get; private set; }
        private List<Action> OnChangeLiseners { get; set; } = new List<Action>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            value = CurrentValue;
            OnChangeLiseners.Add(() => Field.Notify(this));
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if(!EqualityComparer<T>.Default.Equals(value, CurrentValue))
            {
                value = CurrentValue;
                HandleChange();
            }
        }

        private void NotifyChange()
        {
            OnChangeLiseners.ForEach(action => action.Invoke());
        }

        protected virtual void HandleChange()
        {
            var errors = EditContext.GetValidationMessages(FieldIdentifier);
            if (errors.Any())
            {
                IsValid = false;
            }
            NotifyChange();
        }
    }
}
