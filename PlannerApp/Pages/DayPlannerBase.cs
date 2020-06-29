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
        protected PlannerItemDTO ModalFormItem; // the item being added/edited in the modal
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
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(ModalFormItem));
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
        }

        protected void BeginAddingItem()
        {
            isItemNew = true;
            ModalFormItem = new PlannerItemDTO();
        }
    }
}
