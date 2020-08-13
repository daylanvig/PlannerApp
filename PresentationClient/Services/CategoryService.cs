using PresentationClient.Models;
using PlannerApp.Shared.Common;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public class CategoryService : ICategoryService
    {   
        private readonly IPlannerItemDataService plannerItemDataService;
        private readonly ICategoryDataService categoryDataService;

        public CategoryService(IPlannerItemDataService plannerItemDataService, ICategoryDataService categoryDataService)
        {
            this.plannerItemDataService = plannerItemDataService;
            this.categoryDataService = categoryDataService;
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
                var total = (int)Math.Round(categoryGrouping.Sum(g => DateTimeHelper.CalculateLength(g.PlannedActionDate, g.PlannedEndTime)));
                categorizedTime.Add(category, total);
            }
            return categorizedTime;
        }
    }
}
