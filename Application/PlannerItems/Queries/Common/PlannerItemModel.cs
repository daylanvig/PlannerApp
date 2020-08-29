using System;

namespace Application.PlannerItems.Queries.Common
{
    public class PlannerItemModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PlannedActionDate { get; set; }
        public DateTimeOffset PlannedEndTime { get; set; }
        public DateTimeOffset? CompletionDate { get; set; }
        public int? CategoryID { get; set; }
    }
}
