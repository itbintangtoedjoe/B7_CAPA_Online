using B7_CAPA_Online.Models;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Scripts.DataAccess
{
    public class DataAccess
    {
        readonly ConnectionStringSettings dbDFIS = ConfigurationManager.ConnectionStrings["CAPAONLINE"];
        public string GetDataPrint(DALModel Model)
        {
            string result;
            using (IDbConnection db = new SqlConnection(dbDFIS.ConnectionString))
            {
                var GetPrintedData = db.Query<dynamic>(Model.SP,
                                new
                                {
                                    Option = Model.Option,
                                    Departemen = Model.Departemen,
                                    Plant = Model.Plant,
                                    Lokasi = Model.Lokasi,
                                    Tahun = Model.Tahun,
                                    JenisPenyimpangan = Model.JenisPenyimpangan,
                                    JenisKeluhan = Model.JenisKeluhan,
                                    Kategori = Model.Kategori,
                                    NO_CAPA = Model.NO_CAPA,
                                    REG_ID = Model.REG_ID
                                },
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(GetPrintedData, Formatting.Indented);
                result = json;
            }

            return result;
        }

        public string InsertData(DALModel Model, DataTable DepartemenDT, DataTable PenyimpanganDT, DataTable PathDT)
        {
            string result;
            using (IDbConnection db = new SqlConnection(dbDFIS.ConnectionString))
            {
                var GetPrintedData = db.Query<dynamic>(Model.SP,
                                new
                                {
                                    KategoriCAPA = Model.KategoriCAPA,
                                    Lokasi = Model.Lokasi,
                                    Departemen = DepartemenDT,
                                    Penyimpangan = PenyimpanganDT,
                                    AreaPIC = Model.AreaPIC,
                                    Deskripsi = Model.Deskripsi,
                                    TriggerCAPA = Model.TriggerCAPA,
                                    PenyimpanganTerkait = Model.PenyimpanganTerkait,
                                    KeluhanTerkait = Model.KeluhanTerkait,
                                    LampiranTerkait = PathDT,
                                    No_QC_Terkait = Model.No_QC_Terkait,
                                    PIC_CAPA = Model.PIC_CAPA

                                },
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(GetPrintedData, Formatting.Indented);
                result = json;
            }

            return result;
        }

        public string InsertFormHeader(AnalisaKondisiModel Model)
        {
            string result;
            using (IDbConnection db = new SqlConnection(dbDFIS.ConnectionString))
            {
                var GetPrintedData = db.Query<dynamic>(Model.SP,
                                new
                                {
                                    Option = Model.Option,
                                    NO_CAPA = Model.NO_CAPA,
                                    Aspect = Model.Aspect,
                                    WSBH = Model.WSBH,
                                    WAH = Model.WAH,
                                    Status = Model.Status,
                                    isParent = Model.isParent,
                                    WHY_Parent = Model.WHY_Parent,
                                    WHY = Model.WHY
                                },
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(GetPrintedData, Formatting.Indented);
                result = json;
            }

            return result;
        }

        public string GetDataFormPIC(AnalisaKondisiModel Model)
        {
            string result;
            using (IDbConnection db = new SqlConnection(dbDFIS.ConnectionString))
            {
                var GetPrintedData = db.Query<dynamic>(Model.SP,
                                new
                                {
                                    Option = Model.Option,
                                    NO_CAPA = Model.NO_CAPA,
                                    Aspect = Model.Aspect,
                                    WSBH = Model.WSBH,
                                    WAH = Model.WAH,
                                    Status = Model.Status,
                                    isParent = Model.isParent,
                                    WHY_Parent = Model.WHY_Parent,
                                    WHY = Model.WHY
                                },
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(GetPrintedData, Formatting.Indented);
                result = json;
            }

            return result;
        }

        public string InsertFormDetail(AnalisaKondisiModel Model)
        {
            string result;
            using (IDbConnection db = new SqlConnection(dbDFIS.ConnectionString))
            {
                var GetPrintedData = db.Query<dynamic>(Model.SP,
                                new
                                {
                                    Option = Model.Option,
                                    NO_CAPA = Model.NO_CAPA,
                                    Aspect = Model.Aspect,
                                    WSBH = Model.WSBH,
                                    WAH = Model.WAH,
                                    Status = Model.Status,
                                    isParent = Model.isParent,
                                    WHY_Parent = Model.WHY_Parent,
                                    WHY = Model.WHY
                                },
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(GetPrintedData, Formatting.Indented);
                result = json;
            }

            return result;
        }
    }

    //internal string GetDataPrint(int v1, char v2, char v3, char v4)
    //{
    //    throw new NotImplementedException();
    //}
}
