using Blazorise;
using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Pages
{
    public class DayPlannerBase : ComponentBase
    {
        [Inject]
        public IPlannerItemDataService PlannerItemDataService { get; set; }

        protected ICollection<PlannerItemDTO> Items;
        protected DateTime ViewingDate = DateTime.Today;
        protected Modal ModalRef;
        protected PlannerItemDTO EditingItem;

        protected override async Task OnInitializedAsync()
        {
            Items = (await PlannerItemDataService.LoadItems(ViewingDate)).ToList();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (ModalRef != null)
            {
                ModalRef.Show();
            }
        }
        protected async Task EditItem()
        {
            var updatedItem = await PlannerItemDataService.EditItem(EditingItem);
            Items.Remove(EditingItem);
            Items.Add(updatedItem);
            HideModal();
        }

        protected void ShowModal(PlannerItemDTO clickedItem)
        {
            EditingItem = clickedItem;
        }

        protected void HideModal()
        {
            EditingItem = null;
        }
    }
}
