using Microsoft.AspNetCore.Components;
using System;

namespace ClientApp.Components.CalendarComponents
{
    public class CalendarDateHeaderBase : ComponentBase
    {
        [Parameter]
        public DateTime Date { get; set; }
    }
}
