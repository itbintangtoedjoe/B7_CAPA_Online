using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using B7_CAPA_Online.Scripts.SMTP;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    [CheckSession]
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
                {"Departemen" , Model.Departemen},
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "AreaLain", Model.Is_AreaLain},
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
                { "id_fk", data.FKID },
                { "status", data.Status },
                { "recordID", data.RecordID },
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Find_Data");
            return Json(result);
        }
        public ActionResult SubmitApproval(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Recipient = DAL.StoredProcedure(parameters, "[dbo].[SP_EMAIL]");

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

            if (list[0].NO_CAPA != null)
            {
                EmailSender emailSender = new EmailSender();
                int index = 0;
                foreach (var value in list)
                {
                    emailSender.SendEmail(new Dictionary<string, object> {
                        {"Nama_Aplikasi", "CAPA" }, // ini hardcode 
                        {"Kategori", "PICReminder" }, // ini hardcode
                        {"KategoriCAPA", value.KategoriCAPA }, // dari list diatas
                        {"ToEmpName", value.ToEmpName }, // dari list diatas
                        {"Recipient", Recipient}, // recipient object list dari atas
                        {"TriggerCAPA", value.TriggerCAPA}, // ini dari list diatas
                        {"Lokasi", value.Lokasi}, // dari list diatas
                        {"StatusCAPA", value.StatusCAPA}, // ini dari list diatas
                        {"RejectReason", value.RejectReason}, // ini dari list diatas
                        {"PIC", value.PIC}, // ini dari list diatas
                        {"CreateBy", value.CreateBy}, // ini dari list diatas
                        {"DeskripsiMasalah", value.DeskripsiMasalah } // ini dari list diatas
                   }, index);
                    index++;
                }
            }
            return Json(Recipient);
        }
        [HttpPost]
        public ActionResult SimpanHasilPelaksanaan()
        {
            try
            {
                //simpan hasil perbaikan (update table perbaikan)
                //var keys = Request.Form.AllKeys;
                var kategori = Request.Form.Get("Kategori");
                //var hasil = Request.Form.Get("Hasil");
                var tindakanID = Request.Form.Get("TindakanID");
                var nomorCAPA = Request.Form.Get("NomorCAPA");
                var updater = Request.Form.Get("Updater");
                //var status = Request.Form.Get("Status");
                //var tindakan = Request.Form.Get("Tindakan");
                //var dueDate = Request.Form.Get("DueDate");
                //var potensiKegagalan = Request.Form.Get("potensiKegagalan");
                //var penyebabPotensi = Request.Form.Get("penyebabPotensi");
                //var alasantindakan = Request.Form.Get("AlasanTindakan");
                //var alasanduedate = Request.Form.Get("AlasanDueDate");
                //var dictionary = new Dictionary<string, object>{
                //    { "kategori", kategori },
                //    { "recordID", tindakanID },
                //    { "empID", updater },
                //    { "hasil", hasil },
                //    { "status", status },
                //    { "tindakan", tindakan },
                //    { "dueDate", dueDate },
                //    {"P_MPenyebab_Hasil", penyebabPotensi },
                //    {"M_PKegagalan_Hasil",potensiKegagalan },
                //    {"alasan_tindakan", alasantindakan },
                //    {"alasan_due_date",alasanduedate}
                //};
                //var parameters = new DynamicParameters(dictionary);s
                //var result = DAL.StoredProcedure(parameters, "SP_Update_Pelaksanaan");
                var attResult = "";
                var action = "Insert Perbaikan";
#pragma warning disable CS0219 // The variable 'tipe' is assigned but its value is never used
                var tipe = "Perbaikan";
#pragma warning restore CS0219 // The variable 'tipe' is assigned but its value is never used
                var b7path = @"\\b7-drive.bintang7.com\File Upload Intranet\CAPA_Online\Perbaikan";
#pragma warning disable CS0219 // The variable 'locpath' is assigned but its value is never used
                var locpath = @"\\b7-dc1webapps\Attachment\Perbaikan\";
#pragma warning restore CS0219 // The variable 'locpath' is assigned but its value is never used
                if (ConfigurationManager.AppSettings["UploadPath"].ToString() == "true")
                {
                    if (kategori.Contains("Pencegahan"))
                    {
                        b7path = @"\\b7-drive.bintang7.com\File Upload Intranet\CAPA_Online\Pencegahan";
                        locpath = @"\\b7-dc1webapps\Attachment\Pencegahan\";
                        action = "Insert Pencegahan";
                        tipe = "Pencegahan";
                    }
                    else if (kategori.Contains("Treatment"))
                    {
                        b7path = @"\\b7-drive.bintang7.com\File Upload Intranet\CAPA_Online\Treatment";
                        locpath = @"\\b7-dc1webapps\Attachment\Treatment\";
                        action = "Insert Treatment";
                        tipe = "Treatment";
                    }
                }
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = updater + '_' + file.FileName;

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    
                    //var path = Path.Combine(b7path, updater + '_' + fileName);
                    //var local = Path.Combine(locpath, updater + '_' + fileName);
                    string path = Path.Combine(b7path, updater + '_' + fileName);
                    //file.SaveAs(local);
                    var pathencrypt = "";
                    if (ConfigurationManager.AppSettings["UploadPath"].ToString() == "true")
                    {
                        var encrypt = C_EncryptPath(path);
                        var data = jss.Deserialize<dynamic>(encrypt);

                        pathencrypt = data["Data"]["EncrptedString"];


                        file.SaveAs(path);
                    }

                    var attDictionary = new Dictionary<string, object>{
                    { "Action", action },
                    { "NomorCAPA", nomorCAPA },
                    { "NIK", updater },
                    { "FKID", tindakanID },
                    { "FileName", fileName },
                    { "FilePath", path },
                    { "EncryptPath", pathencrypt },
                };
                    var attParameters = new DynamicParameters(attDictionary);
                    attResult = DAL.StoredProcedure(attParameters, "SP_Attachment_Pelaksanaan");

                }

                return Json(attResult);
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, object>{
                    { "ErrorLog", ex.Message },
                    { "StatusID", 6 },
                    { "NO_CAPA", "" },
                    { "UpdateBy", "" }
                };
                var param = new DynamicParameters(dict);
                var result = DAL.StoredProcedure(param, "SP_ERROR_HISTORY");

                return Json(result);
            }
            //insert attachment + save to b7drive

        }
        public string C_EncryptPath(string pathFile)
        {
            var client = new RestClient("https://portal.bintang7.com/CommonService/api/Encrypt");
            var request = new RestRequest(Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");

            FindCAPAModel enprops = new FindCAPAModel();
            enprops.StringSend = pathFile;

            request.AddBody(enprops);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var response = client.Post(request);
            var content = response.Content; // Raw content as string
            string json = content;
            var result = JsonConvert.DeserializeObject<dynamic>(json);

            return json;
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

            //string path;
            //hapus dari b7drive belum dibuat
            //// Updater = path file dari ajax
            ///
            var path = @""+data.Updater+"";
            try 
            {
                System.IO.File.Delete(path); 
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, object>{
                    { "ErrorLog", ex.Message },
                    { "StatusID", 6 },
                    { "NO_CAPA", "" },
                    { "UpdateBy", "" }
                };
                var param = new DynamicParameters(dict);
                result = DAL.StoredProcedure(param, "SP_ERROR_HISTORY");

                return Json(result);
            }
            return Json(result);
        }

        public ActionResult PelaksanaTambahPerbaikan()
        {
            return View();
        }

    }
}