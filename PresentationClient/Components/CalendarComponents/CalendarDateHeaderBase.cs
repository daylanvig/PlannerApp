using Microsoft.AspNetCore.Components;
using System;

namespace PresentationClient.Components.CalendarComponents
{
    public class CalendarDateHeaderBase : ComponentBase
    {
        [Parameter]
        public DateTime Date { get; set; }
    }
}
