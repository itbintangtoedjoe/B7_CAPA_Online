using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public partial class DALModel
    {
        public int Option { get; set; }
        public string Departemen { get; set; }
        public string NO_CAPA { get; set; }
        public string REG_ID { get; set; }
        public string Plant { get; set; }
        public string JenisKeluhan { get; set; }
        public string Lokasi { get; set; }
        public string Tahun { get; set; }
        public string JenisPenyimpangan { get; set; }
        public string Kategori { get; set; }
        public string SP { get; set; }
    }

    public partial class DALModel
    {
        public string TriggerCAPA { get; set; }
        public string PenyimpanganTerkait { get; set; }
        public string KeluhanTerkait { get; set; }
        public string No_QC_Terkait { get; set; }
        public List<Lampiran> LampiranTerkait { get; set; }
        public string KategoriCAPA { get; set; }
        public string Deskripsi { get; set; }
        public string AreaPIC { get; set; }
        public string PIC_CAPA { get; set; }   
        public string PIC_ID { get; set; }
        public List<string> DepartemenCollection { get; set; }
        public List<string> PenyimpanganCollection { get; set; }
    }

    public partial class Dept
    {
        public string Departemen { get; set; }
    }

    public partial class Penyimpangan
    {
        public string PENYIMPANGAN_ID { get; set; }
    }

    public partial class Lampiran
    {
        public string LAMPIRAN_TERKAIT { get; set; }
    }
}