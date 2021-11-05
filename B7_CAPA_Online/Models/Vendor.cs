using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string VendorName { get; set; }
        public int TypeID { get; set; }
        public string VendorType { get; set; }
        public string SuperiorGroupName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int IsActive { get; set; }

    }
}