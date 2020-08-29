using Application.Categories.Queries.Common;
using ClientApp.Models;
using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Services
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

        public async Task<IDictionary<CategoryModel, int>> GetTotalMinutesByCategory()
        {
            var completedItems = await plannerItemDataService.LoadCompletedItems();
            var categories = await categoryDataService.LoadCategories();
            var categorizedTime = new Dictionary<CategoryModel, int>();

            foreach (var categoryGrouping in completedItems.GroupBy(c => c.CategoryID))
            {
                CategoryModel category;
                if (categoryGrouping.Key.HasValue)
                {
                    category = categories.First(c => c.ID == categoryGrouping.Key.Value);
                }
                else
                {
                    category = new CategoryModel
                    {
                        Description = "Uncategorized",
                        Colour = UIConstants.DEFAULT_CATEGORY_COLOUR
                    };
                }
                var total = (int)Math.Round(categoryGrouping.Sum(g => DateTimeHelper.CalculateLength(g.PlannedActionDate.LocalDateTime, g.PlannedEndTime.LocalDateTime)));
                categorizedTime.Add(category, total);
            }
            return categorizedTime;
        }
    }
}
