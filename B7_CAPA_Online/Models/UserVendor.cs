using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class UserVendor
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string UserVendorName { get; set; }
        public Vendor VendorData { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SuperiorID { get; set; }
        public string SuperiorName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int IsActive { get; set; }
    }
}