using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class Email
    {
        public int ID { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }

    public class Recipients
    {
        public string NO_CAPA { get; set; }
        public string Email { get; set; }
        public string KategoriCAPA { get; set; }
        public string ToEmpName { get; set; }
        public string TriggerCAPA { get; set; }
        public string Lokasi { get; set; }
        public string StatusCAPA { get; set; }
        public string PIC { get; set; }
        public string DeskripsiMasalah { get; set; }
        public string CreateBy { get; set; }
    }
}
