using Domain.Common;
using Domain.Categories;
using System;

namespace Domain.PlannerItems
{
    public class PlannerItem : Entity
    {
        public string Description { get; set; }
        public DateTime PlannedActionDate { get; set; }
        public DateTime PlannedEndTime { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
