using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository.Models
{
    public class BuildingSite
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CutomerEmail { get; set; }
        public string BuildingSiteLocation { get; set; }
        public string PhotoFolderUri { get; set; }
    }
}
