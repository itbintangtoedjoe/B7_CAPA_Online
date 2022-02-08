using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class KoordinatorModel
    {

        public class InsertPenyimpangan
        {
            public List<InsertPenyimpanganAttribute> Penyimpangan { get; set; }

        }

        public class InsertPenyimpanganAttribute
        {
            public string NoCapa { get; set; }
            public string PenyimpanganID { get; set; }
            public string Description { get; set; }
            public string Creator { get; set; }

        }


        public class ShowDeviation
        {
            public string NoCapa { get; set; }
            public int Option { get; set; }
            public string Departemen { get; set; }
            public string JenisPenyimpangan { get; set; }
            public string Kategori { get; set; }
            public string JenisKeluhan { get; set; }
            public string Plant { get; set; }
            public string Tahun { get; set; }
            public string Kode { get; set; }
            public string Lokasi { get; set; }
        }
        public class RejectAttribute
        {
            public string NoCAPa { get; set; }
            public string RejectReason { get; set; }
        }

        public class Root
        {
            public Dictionary<string, RootAttribute> RootCause { get; set; }
        }
        public class RootAttribute
        {
            public string key { get; set; }
            public string parent { get; set; }
            public string type { get; set; }
            public string desc { get; set; }
        }

        public class DynamicModel
        {
            public Dictionary<string, object> Model { get; set; }

        }

    }
}