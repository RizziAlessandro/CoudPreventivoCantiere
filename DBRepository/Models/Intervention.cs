using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Notes { get; set; }
        public float Cost { get; set; }
        public string PhotoFolderUri { get; set; }
        public int BuidingSiteId { get; set; }
    }
}
