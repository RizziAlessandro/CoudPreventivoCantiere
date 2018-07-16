using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAppCantiere.Models.InterventionModels
{
    public class InterventionInsertViewModel
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public string Notes { get; set; }
        public float Cost { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
