using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        [Inject]
        public IPlannerItemService PlannerItemService { get; set; }
        
        protected ICollection<PlannerItemDTO> Items;
        [Parameter]
        public DateTime ViewingDate { get; set; } = DateTime.Today;
        protected Modal ModalRef;
        protected PlannerItemDTO ModalFormItem; // the item being added/edited in the modal
        protected CategoryDTO ModalFormCategory;
        protected bool isItemNew = false;

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

        protected async Task SavePlannerItem()
        {
            if (isItemNew)
            {
                Items.Add(await PlannerItemDataService.AddItem(ModalFormItem));
                isItemNew = false;
            }
            else
            {
                var updatedItem = await PlannerItemDataService.EditItem(ModalFormItem);
                Items.Remove(ModalFormItem);
                Items.Add(updatedItem);
            }
            HideModal();
        }

        protected void ShowModal(PlannerItemDTO clickedItem)
        {
            ModalFormItem = clickedItem;
        }

        protected void HideModal()
        {
            ModalFormItem = null;
            ModalFormCategory = null;
        }

        protected void BeginAddingItem()
        {
            isItemNew = true;
            ModalFormItem = new PlannerItemDTO();
        }

        protected async Task ChangeViewingDate(ChangeEventArgs e)
        {
            ViewingDate = DateTime.Parse(e.Value.ToString());
            Items = (await PlannerItemDataService.LoadItems(ViewingDate)).ToList();
        }
    }
}
