using Domain.Common;
using Domain.Categories;
using System;

namespace Domain.PlannerItems
{
    public class PlannerItem : Entity
    {
        public string Description { get; set; }
        public DateTimeOffset PlannedActionDate { get; set; }
        public DateTimeOffset PlannedEndTime { get; set; }
        public DateTimeOffset? CompletionDate { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
