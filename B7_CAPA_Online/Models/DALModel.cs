using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
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
        //22/10/2021
        public string Create_By { get; set; }
        public string Email { get; set; }
        //25/10/2021
        public string UserSupVDRID { get; set; }
        public int RecordID { get; set; }
        public string Type { get; set; }

    }

    public partial class DALModel
    {
        [Required]
        public string TriggerCAPA { get; set; }
        public string PenyimpanganTerkait { get; set; }
        public string KeluhanTerkait { get; set; }
        public string No_QC_Terkait { get; set; }
        public string No_CAPA_Terkait { get; set; }
        [Required]
        public List<Lampiran> LampiranTerkait { get; set; }
        [Required]
        public string KategoriCAPA { get; set; }
        [Required]
        public string KodeCAPA { get; set; }
        [Required]
        public string KataKunci { get; set; }
        [Required]
        public string Deskripsi { get; set; }
        [Required]
        public string AreaPIC { get; set; }
        [Required]
        public string PIC_CAPA { get; set; }
        [Required]
        public string PIC_ID { get; set; }
        [Required]
        public List<string> DepartemenCollection { get; set; }
        [Required]
        public List<string> PenyimpanganCollection { get; set; }
        [Required]
        public List<string> PICCollection { get; set; }
        public string AlasanReject { get; set; }
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
        public string FILE_NAME { get; set; }
    }

}