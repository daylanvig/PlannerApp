using System;

namespace Application.PlannerItems.Queries.Common
{
    public class PlannerItemModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime PlannedActionDate { get; set; }
        public DateTime PlannedEndTime { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? CategoryID { get; set; }
    }
}
