using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlannerApp.Shared.Models
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Colour { get; set; }
    }
}
