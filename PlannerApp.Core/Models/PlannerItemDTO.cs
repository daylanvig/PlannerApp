using System;
using System.ComponentModel.DataAnnotations;

namespace PlannerApp.Shared.Models
{
    public class PlannerItemDTO
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        
        [Required] // nullable to be able to create the input without prefilling, but required so it gets set
        public DateTime? PlannedActionDate { get; set; }
    }
}
