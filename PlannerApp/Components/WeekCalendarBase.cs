using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
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

namespace PlannerApp.Client.Components
{
    public class WeekCalendarBase : ComponentBase
    {
        [Inject]
        public AppState AppState { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject]
        public IPlannerItemService PlannerItemService { get; set; }
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }
        [Inject]
        public IModalService ModalService { get; set; }
        public DateTime ViewingWeekOf = DateTimeHelper.GetMostRecentDayOfWeek(DateTime.Today, DayOfWeek.Sunday);

        protected ICollection<PlannerItemDTO> Items;

        private IEnumerable<CategoryDTO> categories;
        protected List<DateTime> ViewingDates = new List<DateTime>();
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
        }

        protected override async Task OnInitializedAsync()
        {
            SetTitle();
            Items = (await PlannerItemDataService.LoadItems(ViewingWeekOf, ViewingWeekOf.AddDays(7))).ToList();
            categories = await CategoryDataService.LoadCategories();
            for(var i = 0; i < 7; i++)
            {
                ViewingDates.Add(ViewingWeekOf.AddDays(i));
            }
        }

        private void SetTitle()
        {
            AppState.UpdateTitle(
                new TitleState(
                    $"<span class='has-text-weight-light has-padding-right-5'>Calendar</span><span class='has-text-weight-semibold'>{ViewingWeekOf:yyyy}</span>",
                    $"<span class='has-text-weight-semibold'>{ViewingWeekOf:MMM}</span>"
                ));
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && DateTime.Now.Hour != 0)
            {
                // scroll to display current time
                ((IJSInProcessRuntime)JSRuntime).InvokeVoid("customScripts.scrollIntoView", $"#interval-{DateTime.Now.Hour - 1}");
            }
        }

        protected string GetCategoryColour(PlannerItemDTO item)
        {
            if (item.CategoryID.HasValue)
            {
                return categories.First(c => c.ID == item.CategoryID).Colour;
            }
            return "";
        }

        private void UpdateItem(PlannerItemDTO item)
        {
            var existing = Items.FirstOrDefault(i => i.ID == item.ID);
            if(existing != null)
            {
                Items.Remove(existing);
            }
            Items.Add(item);
            ModalService.Close();
        }

        protected void AddItemAtTime(DateTime date, int startHour)
        {
            ShowModal(new PlannerItemDTO
            {
                PlannedActionDate = new DateTime(date.Year, date.Month, date.Day, startHour, 0, 0)
            });
        }
        protected void ShowModal(PlannerItemDTO item)
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "aside");
                builder.AddAttribute(0, "class", "box");
                builder.AddAttribute(1, "style", "overflow-y: auto"); // todo move this elsewhere once done testing
                builder.OpenComponent<PlannerItemForm>(1);
                builder.AddAttribute(1, "Item", item);
                builder.AddAttribute(2, "OnItemSaveCallback", EventCallback.Factory.Create<PlannerItemDTO>(this, UpdateItem));
                builder.CloseComponent();
                builder.CloseElement();
            });
            ModalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }
    }
}
