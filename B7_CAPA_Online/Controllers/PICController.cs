using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using B7_CAPA_Online.Scripts.SMTP;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    [CheckSession]
    public class PICController : Controller
    {
        public readonly SessionChecker checker = new SessionChecker();        
        public static DataTable DT = new DataTable();
        // GET: PIC               
        DataAccess DAL = new DataAccess();

        #region View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FormCAPA(string NoCAPA)
        {
            if(!checker.Checker())
            {
                return Redirect("../Login");
            }
            var list = new ListKondisi();
            list.ClearDT();
            return View();
        }

        #endregion

        #region Populate
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
        public ActionResult GetKajianResiko(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }
        public ActionResult GetDepartemenKajianResiko(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_SHOW_DDL]");
            return Json(Return);
        }
        public ActionResult GetVendorList(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_SHOW_DDL]");
            return Json(Return);
        }
        public ActionResult GetPICKajianResiko(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_SHOW_DDL]");
            return Json(Return);
        }
        public ActionResult CheckStatusCAPA(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }
        public ActionResult GetWhyList(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }

        #endregion

        #region Execute
        public ActionResult UpdateIsArealain(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }
        public ActionResult AddKondisi(AnalisaKondisiModel Model)
        {
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<AnalisaKondisiModel> newList = new List<AnalisaKondisiModel>();
            newList.Add(Model);
            var list = new ListKondisi();
            //var result = list.ToDataTable<AnalisaKondisiModel>(newList); // Insert to DataTable
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
            string Return = DAL.ExecuteFormPIC(Model, dictionary); // Insert to WAHHeader 

            //foreach (DataRow dr in result.Rows)
            //{
            //    row = new Dictionary<string, object>();
            //    foreach (DataColumn col in result.Columns)
            //    {
            //        row.Add(col.ColumnName, dr[col]);                   
            //    }
            //    rows.Add(row);
            //}
            return Json(Return);
        }
        public ActionResult AddWhy(AnalisaKondisiModel Model)
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
        public ActionResult DeleteKondisi(AnalisaKondisiModel Model)
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
        public ActionResult DeleteKajianResiko(AnalisaKondisiModel Model)
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
            DynamicParameters parameters = new DynamicParameters(dictionary);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
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
                { "AreaLain", Model.AreaLain},
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "Departemen", Model.Departemen},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause},
                { "Create_By", Model.Create_By},
                { "RecordID", Model.RecordID},
                { "Status_ID", Model.Status_ID}
            };

            string Return = DAL.ExecuteFormPIC(Model, dictionary);
            return Json(Return);
        }
        public ActionResult AddKajian(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_INSERT_KAJIAN_RESIKO]");
            return Json(Return);
        }
        public ActionResult SubmitToAtasanPIC(DynamicModel Param)
        {
            if(!checker.Checker())
            {
                return Json(JsonConvert.SerializeObject(new { IS_VALID = false, result = "Redirect", url = "../Login"}));
            }

            DynamicParameters parameters = new DynamicParameters(Param.Model);
            var Recipient = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");

            var list = new List<Recipients>();
            var objects = JsonConvert.DeserializeObject(Recipient.ToString()); // parse as array  

            foreach (var item in ((JArray)objects))
            {
                list.Add(new Recipients
                {
                    NO_CAPA = item.Value<string>("NoCAPA")
                    ,
                    Email = item.Value<string>("Email")
                    ,
                    KategoriCAPA = item.Value<string>("KategoriCAPA")
                    ,
                    ToEmpName = item.Value<string>("ToEmpName")
                    ,
                    TriggerCAPA = item.Value<string>("TriggerCAPA")
                    ,
                    Lokasi = item.Value<string>("Lokasi")
                    ,
                    StatusCAPA = item.Value<string>("StatusCAPA")
                    ,
                    PIC = item.Value<string>("PIC")
                    ,
                    RejectReason = item.Value<string>("RejectReason")
                    ,
                    DeskripsiMasalah = item.Value<string>("DeskripsiMasalah")
                    ,
                    CreateBy = item.Value<string>("Requestor")
                });
            }

            // Method SMTP Email
            if (list[0].NO_CAPA != null)
            {
                EmailSender emailSender = new EmailSender();
                int index = 0;
                emailSender.SendEmail(new Dictionary<string, object> {
                    {"Nama_Aplikasi", "CAPA" },
                    {"Kategori", "PICReminder" },
                    {"KategoriCAPA", list[0].KategoriCAPA },
                    {"ToEmpName", list[0].ToEmpName },
                    {"Recipient", Recipient},
                    {"TriggerCAPA", list[0].TriggerCAPA},
                    {"Lokasi", list[0].Lokasi},
                    {"StatusCAPA", "2"},
                    {"PIC", list[0].PIC},
                    {"RejectReason", list[0].RejectReason},
                    {"CreateBy", list[0].CreateBy},
                    {"DeskripsiMasalah", list[0].DeskripsiMasalah }
                }, index);
                index++;
            }

            return Json(Recipient);
        }
        public ActionResult DeleteWhy(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }
        public ActionResult SaveImage(string nocapa, string tipe)
        {
            var b7path = @"\\b7-drive.bintang7.com\Intranetportal\Intranet Attachment\QS\CAPA\DiagramCAPA";
            string Return = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = file.FileName;
                //var path = Path.Combine(b7path, fileName);
                string path = Path.Combine(@"\\b7-dc1webapps\DiagramCAPA\", Path.GetFileName(file.FileName));
                file.SaveAs(path);

                var dictionary = new Dictionary<string, object>{
                { "Action", "Add Diagram" },
                { "NomorCAPA",nocapa},
                {"FilePath",path},
                { "Tipe", tipe }
                 };
                DynamicParameters parameters = new DynamicParameters(dictionary);
                Return = DAL.StoredProcedure(parameters, "[dbo].[SP_Attachment_Pelaksanaan]");


            }

            return Json(Return);
        }
        //public ActionResult KillSession()
        //{
        //    var cookie = Session;
        //    cookie.Clear();
        //    return Json("success");
        //}
        #endregion


    }
}