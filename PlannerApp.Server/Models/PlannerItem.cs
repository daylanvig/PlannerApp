using System;

namespace PlannerApp.Server.Models
{
    public class PlannerItem : Entity
    {
        public string Description { get; set; }
        public DateTime PlannedActionDate { get; set; }
    }
}
