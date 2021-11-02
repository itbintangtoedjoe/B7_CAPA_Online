using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static B7_CAPA_Online.Models.KoordinatorModel;

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
        public int FKID { get; set; }
        public string Status { get; set; }
        public int RecordID { get; set; }
    }
    public class SPUpdatePelaksanaanParams
    {
        public string Kategori { get; set; }
        public int PerbaikanID { get; set; }
        public string Hasil { get; set; }
        public string Updater { get; set; }
        public string IDAttachment { get; set; }
        public HttpPostedFileBase[] Attachments { get; set; }
        public string TindakanPerbaikan { get; set; }
        public DateTime DueDatePerbaikan { get; set; }
        public string TindakanPencegahan { get; set; }
        public DateTime DueDatePencegahan { get; set; }
        public string IsAreaLain { get; set; }
        public List<DynamicModel> KajianResikoList { get; set; }
    }
}