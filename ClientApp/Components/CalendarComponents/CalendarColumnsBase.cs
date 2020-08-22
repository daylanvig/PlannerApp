using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ClientApp.Components.CalendarComponents
{
    public class CalendarColumnsBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private List<CalendarColumn> Columns = new List<CalendarColumn>();

    }
}
