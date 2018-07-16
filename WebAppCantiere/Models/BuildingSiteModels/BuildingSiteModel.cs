using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAppCantiere.Models.BuildingSiteModels
{
    public class BuildingSiteModel
    {
        public string CustomerName { get; set; }
        public string CustomerEMail { get; set; }
        public string BuildingSiteLocation {get; set;}
        public List<IFormFile> Photos { get; set; }
    }
}
