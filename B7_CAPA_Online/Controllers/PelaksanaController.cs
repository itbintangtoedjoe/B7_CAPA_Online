using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            var dictionary = new Dictionary<string, object>{
                { "kategori", kategori },
                { "recordID", tindakanID },
                { "empID", updater },
                { "hasil", hasil },
                { "status", status },
                { "tindakan", tindakan },
                { "dueDate", dueDate },

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

            //insert attachment + save to b7drive
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = file.FileName;

                var path = Path.Combine(@"\\b7-drive.bintang7.com\IntranetPortal\Intranet Attachment\QS\CAPA\"+tipe, fileName);
                file.SaveAs(path);

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