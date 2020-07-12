using Microsoft.AspNetCore.Components;

namespace UIComponents.Bulma
{
    public class FieldBase : UIComponentBase
    {         
        [Parameter]
        public string Label { get; set; }
        // If no need for validation, can just use child content to save having to add <ControlContent> tag
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public RenderFragment ControlContent { get; set; }
        [Parameter]
        public RenderFragment ValidationContent { get; set; }
        protected bool isFieldValid = true;
        public void Notify<T>(InputBase<T> input)
        {
            isFieldValid = input.IsValid;
            StateHasChanged();
        }
    }
}
