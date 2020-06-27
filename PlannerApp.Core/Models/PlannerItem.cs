using System;
using System.ComponentModel.DataAnnotations;

namespace PlannerApp.Core.Models
{
    public class PlannerItem
    {
        [Required]
        public string Description { get; set; }
        
        [Required] // nullable to be able to create the input without prefilling, but required so it gets set
        public DateTime? PlannedActionDate { get; set; }
    }
}
