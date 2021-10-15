using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class VendorType
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int IsActive { get; set; }
    }
}