using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBRepository.Models;

namespace WebAppElettricisti.Models.InterventionModels
{
    public class InterventionViewModel
    {
        public InterventionViewModel()
        {

        }
        public InterventionViewModel(InterventionModel intervention)
        {
            this.InterventionTitle = intervention.InterventionTitle;
            this.CustomerName = intervention.CustomerName;
            this.InterventionCost = intervention.InterventionCost;
        }

        public string InterventionTitle { get; set; }
        public string CustomerName { get; set; }
        public float InterventionCost { get; set; }
    }
}
