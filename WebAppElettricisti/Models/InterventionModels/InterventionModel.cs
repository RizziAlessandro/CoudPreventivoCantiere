using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppElettricisti.Models.InterventionModels
{
    public class InterventionModel
    {
        public int InterventionId { get; set; }
        public string InterventionTitle { get; set; }
        public string CustomerName { get; set; }
        public float InterventionCost { get; set; }
    }
}
