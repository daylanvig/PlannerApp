using Domain.Common;
using System.Collections.Generic;

namespace Domain.Categories
{
    public class Category : Entity
    {
        public string Description { get; set; }
        public string Colour { get; set; }
        public ICollection<PlannerItems.PlannerItem> PlannerItems { get; set; }
    }
}
