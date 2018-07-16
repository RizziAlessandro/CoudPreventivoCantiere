using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBRepository.Models;

namespace WebAppCantiere.Models.BuildingSiteModels
{
    public class BuildingSiteViewModel
    {
        public BuildingSiteViewModel()
        {

        }
        public BuildingSiteViewModel(BuildingSite buildingSite)
        {
            this.Id = buildingSite.Id;
            this.CustomerName = buildingSite.CustomerName;
            this.BuildingSiteLocation = buildingSite.BuildingSiteLocation;
        }
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string BuildingSiteLocation { get; set; }
    }
}
