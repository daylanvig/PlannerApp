using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Server.Models
{
    public class Category : Entity
    {
        public string Description { get; set; }
        public string Colour { get; set; }
        public ICollection<PlannerItem> PlannerItems { get; set; }
    }
}
