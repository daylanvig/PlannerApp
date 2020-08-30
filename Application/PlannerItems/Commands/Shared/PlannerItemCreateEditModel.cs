using System;

namespace Application.PlannerItems.Commands.Shared
{
    public class PlannerItemCreateEditModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        // nullable to be able to create the input without prefilling, but required so it gets set
        public DateTimeOffset? PlannedActionDate { get; set; }
        public DateTimeOffset? PlannedEndTime { get; set; }
        public DateTimeOffset? CompletionDate { get; set; }
        public int? CategoryID { get; set; }
    }
}
