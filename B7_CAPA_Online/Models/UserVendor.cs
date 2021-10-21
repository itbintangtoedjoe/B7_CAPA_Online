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

    public class UserKaryawan
    {
        public string NIK { get; set; }
        public string Username { get; set; }
        public string Departemen { get; set; }
        public string Lokasi { get; set; }
        public string Email { get; set; }
    }

    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TipeLogin { get; set; }
    }
    public class SPFindDataParams
    {
        public string Kategori { get; set; }
        public string NomorCAPA { get; set; }
        public string NIK { get; set; }
    }
}