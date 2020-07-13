using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using PlannerApp.Client.Components;
using PlannerApp.Client.Services;
using PlannerApp.Client.Store.ChangePageUseCase;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Bulma;
using UIComponents.Bulma.Helpers;
using UIComponents.Services;

namespace PlannerApp.Client.Pages
{
    public class DayPlannerBase : ComponentBase
    {
        [Inject] AppState AppState { get; set; }
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IModalService ModalService { get; set; }
        [Inject]
        protected IPlannerItemService PlannerItemService { get; set; }
        protected ICollection<PlannerItemDTO> Items;
        [Parameter]
        public DateTime ViewingDate { get; set; } = DateTime.Today;
        protected Modal ModalRef;
        protected PlannerItemDTO ModalFormItem; // the item being added/edited in the modal

        protected override async Task OnInitializedAsync()
        {
            AppState.UpdateTitle(new TitleState(DateTimeHelper.FormatFullDate(ViewingDate)));
            Items = (await PlannerItemDataService.LoadItems(ViewingDate)).ToList();
        }

        protected void SavePlannerItem(PlannerItemDTO plannerItemDTO)
        {
            var existingItem = Items.FirstOrDefault(i => i.ID == plannerItemDTO.ID);
            if(existingItem != null)
            {
                Items.Remove(existingItem);
            }
            Items.Add(plannerItemDTO);
            HideModal();
        }

        protected void ShowModal(PlannerItemDTO clickedItem)
        {
            ModalFormItem = clickedItem;
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "aside");
                builder.AddAttribute(0, "class", "box");
                builder.AddAttribute(1, "style", "overflow-y: auto"); // todo move this elsewhere once done testing
                builder.OpenComponent<PlannerItemForm>(1);
                builder.AddAttribute(1, "Item", ModalFormItem);
                builder.AddAttribute(2, "OnItemSaveCallback", EventCallback.Factory.Create<PlannerItemDTO>(this, SavePlannerItem));
                builder.CloseComponent();
                builder.CloseElement();
            });
            ModalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }

        protected void HideModal()
        {
            ModalService.Close();
        }

        protected void BeginAddingItem()
        {
            ShowModal(new PlannerItemDTO());
        }

        protected void BeginAddingCategory()
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenComponent<CategoryForm>(0);
                builder.AddAttribute(1, "Category", new CategoryDTO());
                builder.AddAttribute(2, "OnSaveCallback", EventCallback.Factory.Create<CategoryDTO>(this, HideModal));
                builder.CloseComponent();
            });
            ModalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }
        protected async Task ChangeViewingDate(ChangeEventArgs e)
        {
            ViewingDate = DateTime.Parse(e.Value.ToString());
            AppState.UpdateTitle(new TitleState(DateTimeHelper.FormatFullDate(ViewingDate)));
            Items = (await PlannerItemDataService.LoadItems(ViewingDate)).ToList();
        }
    }
}
