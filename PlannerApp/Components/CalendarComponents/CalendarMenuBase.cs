using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Models;
using System;
using UIComponents.Bulma;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarMenuBase : ComponentBase
    {
        [Parameter]
        public CalendarState State { get; set; }
        [Parameter]
        public EventCallback OnStateChange { get; set; }
        protected SelectField<CalendarMode> ModeInput;
        protected DateField DateInput;

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            ModeInput.Subscribe(async () => await OnStateChange.InvokeAsync(null));
            DateInput.Subscribe(async () => await OnStateChange.InvokeAsync(null));
        }
    }
}
