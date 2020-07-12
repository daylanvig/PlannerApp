using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public class PlannerItemFormBase : ComponentBase
    {
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Parameter]
        public PlannerItemDTO Item { get; set; }
        [Parameter]
        public EventCallback<EditContext> OnValidCallback { get; set; }

        protected IEnumerable<CategoryDTO> Categories = new List<CategoryDTO>();

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryDataService.LoadCategories();
        }
    }
}
