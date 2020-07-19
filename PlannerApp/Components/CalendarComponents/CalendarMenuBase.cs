using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Models;
using System;

namespace PlannerApp.Client.Components.CalendarComponents
{
    public class CalendarMenuBase : ComponentBase
    {
        [Parameter]
        public CalendarState State { get; set; }
    }
}
