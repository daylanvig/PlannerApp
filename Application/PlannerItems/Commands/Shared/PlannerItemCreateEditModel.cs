﻿using System;

namespace Application.PlannerItems.Commands.Shared
{
    public class PlannerItemCreateEditModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        // nullable to be able to create the input without prefilling, but required so it gets set
        public DateTime? PlannedActionDate { get; set; }
        public DateTime? PlannedEndTime { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? CategoryID { get; set; }
    }
}