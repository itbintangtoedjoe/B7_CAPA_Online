using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class FindCAPAModel
    {
        public string REG_ID { get; set; }
        public string NO_CAPA { get; set; }
        public string TRIGGER_CAPA { get; set; }
        public string KATEGORI_CAPA { get; set; }
        public string KATEGORI_CAPA2 { get; set; }
        public string DESKRIPSI_MASALAH { get; set; }
        public string LOKASI_CAPA { get; set; }
        public string LOKASI_CAPA2 { get; set; }
        public string AREA_PIC { get; set; }
        public string PIC_CAPA { get; set; }
        public string REJECT_REASON { get; set; }
        public string STATUS_ID { get; set; }
        public string DEPARTEMEN { get; set; }
        public string FILE_PATH { get; set; }
        public string PENYIMPANGAN { get; set; }
        public string DEPTLIST { get; set; }
        public string FILENAMES { get; set; }
        public List<string> PENYIMPANGANLIST { get; set; }
        public List<string> FILELIST { get; set; }
        public List<string> FILE_NAME { get; set; }
        public string FullName { get; set; }
        public string Kata_Kunci { get; set; }
        public string Kode_CAPA { get; set; }
        public string Kode_CAPA2 { get; set; }
        public string EMAIL { get; set; }

    }
}