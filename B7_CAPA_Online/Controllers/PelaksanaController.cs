using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    public class PelaksanaController : Controller
    {
        readonly DataAccess DAL = new DataAccess();
        // GET: Pelaksana
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PelaksanaCAPA()
        {
            return View();
        }

        public ActionResult PelaksanaCAPAPembatalan()
        {
            return View();
        }
        public ActionResult PelaksanaCAPAPerubahan()
        {
            return View();
        }

        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname));

        }
        public ActionResult GetDataPIC(AnalisaKondisiModel Model)
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause},
                { "Create_By", Model.Create_By},
                { "RecordID", Model.RecordID}
            };
            string Result = DAL.GetDataFormPIC(Model, dictionary);
            return Json(Result);
        }
        public ActionResult AddTindakan(AnalisaKondisiModel Model)
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause},
                { "Create_By", Model.Create_By},
                { "RecordID", Model.RecordID}
            };

            string Return = DAL.ExecuteFormPIC(Model, dictionary);
            return Json(Return);
        }
        public ActionResult FindCAPADetail(SPFindDataParams data)
        {
            var dictionary = new Dictionary<string, object>{
                { "kategori", data.Kategori },
                { "nomorCAPA", data.NomorCAPA },
                { "nik", data.NIK },
                { "id_fk", data.FKID },
                { "status", data.Status },
                { "recordID", data.RecordID },
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Find_Data");
            return Json(result);
        }

        [HttpPost]
        public ActionResult SimpanHasilPelaksanaan()
        {
            //simpan hasil perbaikan (update table perbaikan)
            //var keys = Request.Form.AllKeys;
            var kategori = Request.Form.Get("Kategori");
            var hasil = Request.Form.Get("Hasil");
            var tindakanID = Request.Form.Get("TindakanID");
            var updater = Request.Form.Get("Updater");
            var nomorCAPA = Request.Form.Get("NomorCAPA");
            var status = Request.Form.Get("Status");
            var tindakan = Request.Form.Get("Tindakan");
            var dueDate = Request.Form.Get("DueDate");
            var potensiKegagalan = Request.Form.Get("potensiKegagalan");
            var penyebabPotensi = Request.Form.Get("penyebabPotensi");
            var alasantindakan = Request.Form.Get("AlasanTindakan");
            var alasanduedate = Request.Form.Get("AlasanDueDate");
            var dictionary = new Dictionary<string, object>{
                { "kategori", kategori },
                { "recordID", tindakanID },
                { "empID", updater },
                { "hasil", hasil },
                { "status", status },
                { "tindakan", tindakan },
                { "dueDate", dueDate },
                {"P_MPenyebab_Hasil", penyebabPotensi },
                {"M_PKegagalan_Hasil",potensiKegagalan },
                {"alasan_tindakan", alasantindakan },
                {"alasan_due_date",alasanduedate}
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Update_Pelaksanaan");

            var action = "Insert Perbaikan";
            var tipe = "Perbaikan";
            if (kategori.Contains("Pencegahan"))
            {
                action = "Insert Pencegahan";
                tipe = "Pencegahan";
            }
            else if (kategori.Contains("Treatment"))
            {
                action = "Insert Treatment";
                tipe = "Treatment";
            }

            //insert attachment + save to b7drive
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = file.FileName;

                //var path = Path.Combine(@"\\b7-drive.bintang7.com\IntranetPortal\Intranet Attachment\QS\CAPA\"+tipe, fileName);
                string path = Path.Combine(Server.MapPath("~/Content/Files/"), Path.GetFileName(file.FileName));
                //file.SaveAs(path);

                var attDictionary = new Dictionary<string, object>{
                    { "Action", action },
                    { "NomorCAPA", nomorCAPA },
                    { "NIK", updater },
                    { "FKID", tindakanID },
                    { "FileName", fileName },
                    { "FilePath", path },
                };
                var attParameters = new DynamicParameters(attDictionary);
                var attResult = DAL.StoredProcedure(attParameters, "SP_Attachment_Pelaksanaan");
            }

            return Json(result);
        }

        public ActionResult DeleteAttachment(SPUpdatePelaksanaanParams data)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            var dictionary = new Dictionary<string, object>{
                { "Action", "Delete" },
                { "IDAttachment", data.IDAttachment },
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Attachment_Pelaksanaan");

            //hapus dari b7drive belum dibuat
            return Json(result);
        }
        public ActionResult PelaksanaTambahPerbaikan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SimpanPerbaikanBaru(SPUpdatePelaksanaanParams data)
        {
            var dictionary = new Dictionary<string, object>{
                { "kategori", "Penambahan Perbaikan" },
                { "tindakan", data.TindakanPerbaikan },
                { "empID", data.Updater },
                { "dueDate", data.DueDatePerbaikan },
                { "isAreaLain", data.IsAreaLain },
                { "recordID", data.PerbaikanID },
                { "tindakanPencegahan", data.TindakanPencegahan },
                { "dueDatePencegahan", data.DueDatePencegahan },
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Update_Pelaksanaan");
            //return Json(result);

            //insert kajian resiko
            var kajianResikoList = data.KajianResikoList;
            string kajianResult="";
            for (int i = 0; i < kajianResikoList.Count; i++)
            {
                DynamicParameters kajianParameters = new DynamicParameters(kajianResikoList[i].Model);
                kajianResult = DAL.StoredProcedure(kajianParameters, "[dbo].[SP_INSERT_KAJIAN_RESIKO]");
            }
            return Json(kajianResult);
            //var dictionaryResiko = new Dictionary<string, object>{
            //    { "kategori", "Penambahan Perbaikan" },
            //    { "tindakan", data.TindakanPerbaikan },
            //    { "empID", data.Updater },
            //    { "dueDate", data.DueDatePerbaikan },
            //    { "isAreaLain", data.IsAreaLain },
            //    { "recordID", data.PerbaikanID },
            //    { "tindakanPencegahan", data.TindakanPencegahan },
            //    { "dueDatePencegahan", data.DueDatePencegahan },
            //};
            //var kajianParameters = new DynamicParameters(dictionaryResiko);
            //var kajianResult = DAL.StoredProcedure(parameters, "SP_INSERT_KAJIAN_RESIKO");
        }
    }
}