using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public class PlannerItemFormBase : ComponentBase
    {
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        
        [Parameter]
        public EventCallback<EditContext> OnValidCallback { get; set; }
    }
}
