using System;

namespace PlannerApp.Server.Models
{
    public class PlannerItem : Entity
    {
        public string Description { get; set; }
        public DateTime PlannedActionDate { get; set; }
        public DateTime PlannedEndTime { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
