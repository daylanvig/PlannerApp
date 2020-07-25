using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private Action OnChange;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            value = CurrentValue;
            OnChange += () => Field.Notify(this);
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
            OnChange?.Invoke();
        }

        public void Subscribe(Action action)
        {
            OnChange += action;
        }

        public void Unsubscribe(Action action)
        {
            OnChange -= action;
        }

        protected virtual void HandleChange()
        {
            var errors = EditContext.GetValidationMessages(FieldIdentifier);
            if (errors.Any())
            {
                IsValid = false;
            }
            Console.WriteLine(Value.ToString());
            NotifyChange();
        }
    }
}
