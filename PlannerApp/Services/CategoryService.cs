using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Components;
using PlannerApp.Client.Models;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;

namespace PlannerApp.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationWideComponentService<ModalParams> modalService;
        private readonly IPlannerItemDataService plannerItemDataService;
        private readonly ICategoryDataService categoryDataService;

        public CategoryService(IApplicationWideComponentService<ModalParams> modalService, IPlannerItemDataService plannerItemDataService, ICategoryDataService categoryDataService)
        {
            this.modalService = modalService;
            this.plannerItemDataService = plannerItemDataService;
            this.categoryDataService = categoryDataService;
        }

        public void BeginAddingCategory(Action<CategoryDTO> onSave)
        {
            var modalBody = new RenderFragment(builder =>
            {
                builder.OpenComponent<CategoryForm>(0);
                builder.AddAttribute(1, "Category", new CategoryDTO());
                builder.AddAttribute(2, "OnSaveCallback", EventCallback.Factory.Create<CategoryDTO>(this, (CategoryDTO savedItem) =>
                {
                    modalService.Close();
                    onSave?.Invoke(savedItem);
                }));
                builder.CloseComponent();
            });
            modalService.Show(new ModalParams(modalBody, style: ModalStyle.Normal));
        }

        public async Task<IDictionary<CategoryDTO, int>> GetTotalMinutesByCategory()
        {
            var completedItems = await plannerItemDataService.LoadCompletedItems();
            var categories = await categoryDataService.LoadCategories();
            var categorizedTime = new Dictionary<CategoryDTO, int>();

            foreach (var categoryGrouping in completedItems.GroupBy(c => c.CategoryID))
            {
                CategoryDTO category;
                if (categoryGrouping.Key.HasValue)
                {
                    category = categories.First(c => c.ID == categoryGrouping.Key.Value);
                }
                else
                {
                    category = new CategoryDTO
                    {
                        Description = "Uncategorized",
                        Colour = UIConstants.DEFAULT_CATEGORY_COLOUR
                    };
                }
                var total = (int)Math.Round(categoryGrouping.Sum(g => DateTimeHelper.CalculateLength(g.PlannedActionDate.Value, g.PlannedEndTime.Value)));
                categorizedTime.Add(category, total);
            }
            return categorizedTime;
        }
    }
}
