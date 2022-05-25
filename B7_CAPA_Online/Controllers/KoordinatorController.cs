using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using B7_CAPA_Online.Scripts.SMTP;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{

    [CheckSession]
    public class KoordinatorController : Controller
    {
        // GET: Koordinator
        //readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["CAPAONLINE"];
        //readonly DataTable DT = new DataTable();
        //SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //string Result;
        public DataTable DT = new DataTable();
        readonly DataTable dtLampiran = new DataTable();
        readonly SessionChecker checker = new SessionChecker();
        DataAccess DAL = new DataAccess();

        #region View
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Koordinator()
        {
            return View();
        }

        public ActionResult ApprovalKoordinator()
        {
            return View();
        }

        public ActionResult ApprovalKoordinator2(string NoCAPA, string StatsID)
        {
            ViewBag.NoCAPA = NoCAPA;
            ViewBag.StatsID = StatsID;
            return View();
        }

        public ActionResult ApprovalKoordinatorCARPAR()
        {
            return View();
        }

        public ActionResult SubmitCARPAR()
        {
            return View();
        }
        public ActionResult MonitoringEfektivitas(string NoCAPA, string status)
        {
            ViewBag.NoCAPA = NoCAPA;
            ViewBag.status = status;
            return View();
        }
        public ActionResult CreateCAPA()
        {
            var listDept = new ListDepartemen();
            var listPIC = new ListPIC();
            listDept.ClearDT();
            listPIC.ClearDT();
            return View();
        }


        public ActionResult PenentuanTindakanPerbaikan()
        {
            return PartialView();
        }
        public ActionResult PenentuanTindakanPencegahan()
        {
            return PartialView();
        }
        public ActionResult InvestigasiPenyimpangan()
        {
            return PartialView();
        }

        public ActionResult KajianRisiko()
        {
            return PartialView();
        }

        public ActionResult TriggerCAPA()
        {
            return PartialView();
        }

        public ActionResult RequestCAPA(string CAPA, string REG)
        {
            var list = new ListDepartemen();
            ViewBag.NoCAPA = CAPA;
            ViewBag.REG = REG;
            list.ClearDT();
            return View();
        }
        #endregion

        #region Populate
        public ActionResult GetKoordinatorDDL(DALModel Model)
        {
            string Result = DAL.GetDataPrint(Model);
            return Json(Result);
        }
        public ActionResult GetPenyimpangan(DALModel Model)
        {
            //Model.JenisPenyimpangan = "";
            //Model.Departemen = "IT";
            string Result = DAL.GetDataPrint(Model);
            return Json(Result);
        }
        public ActionResult GetKategoriPenyimpangan(DALModel Model)
        {
            string Result = DAL.GetDataPrint(Model);
            return Json(Result);
        }
        public ActionResult GetPlantCCC(DALModel Model)
        {
            string Result = DAL.GetDataPrint(Model);
            return Json(Result);
        }
        public JsonResult GetDepartments(DALModel Model)
        {
            string Result;
            return Json(Result = DAL.GetDataPrint(Model));
        }
        public JsonResult GetSupervisors(DALModel Model)
        {
            string Result;
            return Json(Result = DAL.GetDataPrint(Model));

        }
        public ActionResult GetEvaluator(string departemen, int Option, string Lokasi, string NoCAPA)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Departemen", departemen},
                {"Option",Option },
                {"Lokasi",Lokasi },
                {"NoCAPA",NoCAPA }
            };
            var spname = "SP_SHOW_EVALUATOR_DDL";

            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));

        }

        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname));

        }


        public ActionResult InsertAttachment(string spname, string NoCapa, string Creator)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LAMPIRAN_TERKAIT");
            dt.Columns.Add("FILE_NAME");
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream; 
                string filePath = Path.Combine(@"\\b7-drive.bintang7.com\Intranetportal\Intranet Attachment\QS\CAPA\Koordinator\", Path.GetFileName(file.FileName));
                file.SaveAs(filePath);
                DataRow rowstype = dt.NewRow();
                rowstype["LAMPIRAN_TERKAIT"] = filePath;
                rowstype["FILE_NAME"] = file.FileName;
                dt.Rows.Add(rowstype);
            }
            var dictionary = new Dictionary<string, object>
            {
                {"NoCapa", NoCapa},
                {"Option", 0},
                {"Creator", Creator}
            };
            var parameters = new DynamicParameters(dictionary);
            parameters.Add("LampiranCollection", dt.AsTableValuedParameter("LampiranCollection"));
            return Json(DAL.StoredProcedure(parameters, spname));
        }

        public ActionResult GetRoot(string NoCapa, string Type)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"NoCapa", NoCapa},
                {"Option", 2 },
                {"Type",Type }
            };
            var spname = "SP_SHOW_ANALISA&ROOT";

            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));

        }

        public ActionResult GetAnalisa(string NoCapa)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"NoCapa", NoCapa},
                {"Option", 1 }
            };
            var spname = "SP_SHOW_ANALISA&ROOT";

            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));

        }
        public ActionResult RejectKoor2(RejectAttribute Model)
        {
            var spname = "SP_REJECT_KOOR2";

            var parameters = new DynamicParameters(Model);
            return Json(DAL.StoredProcedure(parameters, spname));

        }
        public ActionResult InsertAddAbleDeviation(InsertPenyimpangan Model)
        {
            var pjg = Model.Penyimpangan.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("NoCapa");
            dt.Columns.Add("PenyimpanganID");
            dt.Columns.Add("Description");
            dt.Columns.Add("Creator");
            int trav;
            for (trav = 0; trav < pjg; trav++)
            {
                DataRow rowstype = dt.NewRow();
                rowstype["NoCapa"] = Model.Penyimpangan[trav].NoCapa;
                rowstype["PenyimpanganID"] = Model.Penyimpangan[trav].PenyimpanganID;
                rowstype["Description"] = Model.Penyimpangan[trav].Description;
                rowstype["Creator"] = Model.Penyimpangan[trav].Creator;
                dt.Rows.Add(rowstype);
            }
            var dictionary = new Dictionary<string, object>
            {
                {"Option",1}

            };
            var parameters = new DynamicParameters(dictionary);
            parameters.Add("InsertPenyimpangan", dt.AsTableValuedParameter("[dbo].[InsertPenyimpangan]"));

            var spname = "SP_Insert_Penyimpangan";

            return Json(DAL.StoredProcedure(parameters, spname));

        }
        public ActionResult InsertAddAbleKoor4(InsertPenyimpangan Model)
        {
            var pjg = Model.Penyimpangan.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("NoCapa");
            dt.Columns.Add("PenyimpanganID");
            dt.Columns.Add("Description");
            dt.Columns.Add("Creator");
            int trav;
            for (trav = 0; trav < pjg; trav++)
            {
                DataRow rowstype = dt.NewRow();
                rowstype["NoCapa"] = Model.Penyimpangan[trav].NoCapa;
                rowstype["PenyimpanganID"] = Model.Penyimpangan[trav].PenyimpanganID;
                rowstype["Description"] = Model.Penyimpangan[trav].Description;
                rowstype["Creator"] = Model.Penyimpangan[trav].Creator;
                dt.Rows.Add(rowstype);
            }
            var parameters = new DynamicParameters();
            parameters.Add("InsertPenyimpangan", dt.AsTableValuedParameter("[dbo].[InsertPenyimpangan]"));
            parameters.Add("Option", 1);
            var spname = "SP_FORM_KOOR4";

            return Json(DAL.StoredProcedure(parameters, spname));

        }

        public ActionResult ShowAddAbleDeviation(ShowDeviation Model)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Option",Model.Option },
                {"NoCapa", Model.NoCapa},
                {"JenisPenyimpangan",Model.JenisPenyimpangan },
                {"Kategori",Model.Kategori },
                {"Departemen",Model.Departemen },
                {"JenisKeluhan",Model.JenisKeluhan },
                {"Plant",Model.Plant},
                {"Tahun",Model.Tahun},
                {"Kode",Model.Kode},
                {"Lokasi",Model.Lokasi}

            };
            var spname = "[dbo].[SP_SHOW_ADDABLE_DEVIATION]";

            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));

        }
        public ActionResult AddDepartments(ListDepartemenModel Model)
        {
            try
            {
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                List<ListDepartemenModel> newList = new List<ListDepartemenModel>();
                newList.Add(Model);
                var list = new ListDepartemen();
                var result = list.ToDataTable<ListDepartemenModel>(newList);
                foreach (DataRow dr in result.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in result.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return Json(rows);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public ActionResult DeleteDepartments(ListDepartemenModel Model)
        {
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<ListDepartemenModel> newList = new List<ListDepartemenModel>();
            newList.Add(Model);
            string department = Convert.ToString(Model.Departemen);
            var list = new ListDepartemen();
            var result = list.DeleteRows(department);
            foreach (DataRow dr in result.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in result.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows);
        }

        public ActionResult AddPIC(List<ListPICModel> Model)
        {
            try
            {
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
               // List<ListPICModel> newList = new List<ListPICModel>();
               // newList.Add(Model);
                var list = new ListPIC();
                var result = list.ToDataTable<ListPICModel>(Model);
                foreach (DataRow dr in result.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in result.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return Json(rows);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public ActionResult DeletePIC(ListPICModel Model)
        {
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<ListPICModel> newList = new List<ListPICModel>();
            newList.Add(Model);
            string PIC = Convert.ToString(Model.PIC);
            var list = new ListPIC();
            var result = list.DeleteRows(PIC);
            foreach (DataRow dr in result.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in result.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows);
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

            //path = data.Updater;
            //System.IO.File.Delete(path);
            return Json(result);
        }

        public ActionResult DeleteAttachmentKoor4(DynamicModel data, string spname)
        {

            var parameters = new DynamicParameters(data.Model);
            var result = DAL.StoredProcedure(parameters, spname);

            //hapus dari b7drive belum dibuat

            return Json(result);
        }

        public ActionResult GetCAPA(DALModel Model)
        {
            FindCAPAModel findModel = new FindCAPAModel();
            string Result;
            int Count = 0;
            List<ListDepartemenModel> arrListDept = new List<ListDepartemenModel>();
            var list = new ListDepartemen();
            string[] attr = { "DEPARTEMEN", "PENYIMPANGAN", "FILE_PATH", "FILENAMES" };
            Result = DAL.GetDataPrint(Model);
            var CAPA = JsonConvert.DeserializeObject<List<FindCAPAModel>>(Result);
            foreach (var str in CAPA)
            {
                foreach (string prop in attr)
                {
                    var val = str.GetType()
                    .GetProperty(prop)
                    .GetValue(str);
                    List<string> newList = new List<string>();
                    if (val != null)
                    {
                        string[] value = val.ToString().Split(',');                      
                        foreach (string temp in value)
                        {
                            newList.Add(temp.Trim());
                            arrListDept.Add(new ListDepartemenModel { Departemen = temp.Trim() });
                        }
                    }                    
                    switch (Count)
                    {
                        case 0:
                            var deptTable = list.ToDataTable<ListDepartemenModel>(arrListDept);
                            string JSONString = string.Empty;
                            JSONString = JsonConvert.SerializeObject(deptTable);
                            str.DEPTLIST = JSONString;
                            break;
                        case 1:
                            str.PENYIMPANGANLIST = newList;
                            break;
                        case 2:
                            str.FILELIST = newList;
                            break;
                        case 3:
                            str.FILE_NAME = newList;
                            break;
                    }
                    Count++;
                }
            }
            return Json(CAPA);
        }

        public ActionResult CheckUser(DynamicModel Param)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(Param.Model);
            string Result = DAL.StoredProcedure(dynamicParameters, "[dbo].[SP_SHOW_DDL]");
            return Json(Result);
        }
        public void ResetDepartments()
        {
            var list = new ListDepartemen();
            list.ClearDT();
        }

        public void ResetPIC()
        {
            var list = new ListPIC();
            list.ClearDT();
        }

        //public JsonResult GetDepartments()
        //{
        //    string Constring = mySetting.ConnectionString;
        //    SqlConnection conn = new SqlConnection(Constring);
        //    List<string> ModelData = new List<string>();
        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand("SP_SHOW_DDL", conn))
        //        {
        //            conn.Open();
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@Option", SqlDbType.VarChar).Value = 2;
        //            dataAdapter.SelectCommand = command;
        //            dataAdapter.Fill(DT);
        //            conn.Close();
        //        }

        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in DT.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in DT.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);
        //        }

        //        return Json(rows);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public JsonResult GetSupervisors(ListDepartemenModel Model)
        //{
        //    string Constring = mySetting.ConnectionString;
        //    SqlConnection conn = new SqlConnection(Constring);
        //    List<string> ModelData = new List<string>();
        //    try
        //    {
        //        if (Model.Departemen == "-")
        //        {
        //            return Json(new[] { new { EmpName = "None" } }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            using (SqlCommand command = new SqlCommand("SP_SHOW_DDL", conn))
        //            {
        //                conn.Open();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@Option", SqlDbType.VarChar).Value = 3;
        //                command.Parameters.Add("@Departemen", SqlDbType.VarChar).Value = Model.Departemen;
        //                command.Parameters.Add("@Lokasi", SqlDbType.VarChar).Value = Model.Lokasi;
        //                dataAdapter.SelectCommand = command;
        //                dataAdapter.Fill(DT);
        //                conn.Close();
        //            }

        //            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //            Dictionary<string, object> row;
        //            foreach (DataRow dr in DT.Rows)
        //            {
        //                row = new Dictionary<string, object>();
        //                foreach (DataColumn col in DT.Columns)
        //                {
        //                    row.Add(col.ColumnName, dr[col]);
        //                }
        //                rows.Add(row);
        //            }
        //            return Json(rows);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}        
        #endregion  

        #region Execute
        [HttpPost]
        public ActionResult InsertCAPA(DALModel Model)
        {
            if (!checker.Checker())
            {
                return Redirect("../Login");
            }
            int fileCount = Request.Files.Count;

            for (int i = 0; i < fileCount; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(@"\\b7-drive.bintang7.com\Intranetportal\Intranet Attachment\QS\CAPA\Koordinator\", fileName);
                //string filePath = Path.Combine(Server.MapPath("~/Content/Files/"), Path.GetFileName(file.FileName));
                Model.LampiranTerkait.Add(new Lampiran { LAMPIRAN_TERKAIT = filePath, FILE_NAME = Path.GetFileName(file.FileName) });

                Model.SP = "[dbo].[SP_CAPA_ID]";       
                if(fileName != "")
                {
                    file.SaveAs(filePath);
                }                
            }

            string jsonObj = string.Join(",", Model.DepartemenCollection.ToArray());
            dynamic deptList = JsonConvert.DeserializeObject<List<Dept>>(jsonObj);

            string penyimpanganObj = string.Join(",", Model.PenyimpanganCollection.ToArray());
            dynamic penyimpanganList = JsonConvert.DeserializeObject<List<Penyimpangan>>(penyimpanganObj);

            string picObj = string.Join(",", Model.PICCollection.ToArray());
            dynamic picList = JsonConvert.DeserializeObject<List<ListPICModel>>(picObj);

            var Departemen_DT = ToDataTable<Dept>(deptList);                      
            var Penyimpangan_DT = new DataTable();
            if (penyimpanganList != null)
            {
                Penyimpangan_DT = ToDataTable<Penyimpangan>(penyimpanganList);
            }
            else
            {
                Penyimpangan_DT.Columns.Add("PENYIMPANGAN_ID", typeof(string));
                DataRow row = Penyimpangan_DT.NewRow();
                row["PENYIMPANGAN_ID"] = "-";
                Penyimpangan_DT.Rows.Add(row);                
            }
            var PIC_DT = ToDataTable<ListPICModel>(picList);
            var Path_DT = ToDataTable<Lampiran>(Model.LampiranTerkait);

            // Method Insert Data
            var Recipient = DAL.InsertData(Model, Departemen_DT, Penyimpangan_DT, Path_DT, PIC_DT);

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
            //EmailSender emailSender = new EmailSender();
            //emailSender.SendEmail(new Dictionary<string, object> {
            //    {"Nama_Aplikasi", "CAPA" },
            //    {"Kategori", "PICReminder" },
            //    {"KategoriCAPA", list[0].KategoriCAPA },
            //    {"ToEmpName", Model.PIC_CAPA },
            //    {"Recipient", Recipient},
            //    {"TriggerCAPA", Model.TriggerCAPA},
            //    {"Lokasi", Model.Lokasi},
            //    {"StatusCAPA", "1"},
            //    {"PIC", Model.PIC_ID},
            //    {"CreateBy", Session["FullName"].ToString()},
            //    {"DeskripsiMasalah", Model.Deskripsi }
            //});

            EmailSender emailSender = new EmailSender();
            int index = 0;
            //emailSender.SendEmail(new Dictionary<string, object> {
            //    {"Nama_Aplikasi", "CAPA" },
            //    {"Kategori", "PICReminder" },
            //    {"KategoriCAPA", list[0].KategoriCAPA },
            //    {"ToEmpName", list[0].ToEmpName },
            //    {"Recipient", Recipient},
            //    {"TriggerCAPA", list[0].TriggerCAPA},
            //    {"Lokasi", list[0].Lokasi},
            //    {"StatusCAPA", "1"},
            //    {"PIC", list[0].PIC},
            //    {"RejectReason", list[0].RejectReason},
            //    {"CreateBy", list[0].CreateBy},
            //    {"DeskripsiMasalah", list[0].DeskripsiMasalah }
            //},index);
            //index++;
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
                    {"StatusCAPA", "1"}, // ini dari list diatas
                    {"RejectReason", value.RejectReason}, // ini dari list diatas
                    {"PIC", value.PIC}, // ini dari list diatas                             
                    {"CreateBy", value.CreateBy}, // ini dari list diatas
                    {"DeskripsiMasalah", value.DeskripsiMasalah } // ini dari list diatas
                }, index);
                index++;
            }

            return RedirectToAction("TaskList", "PendingTask", new { success = "succeed" });
        }

        //public DataTable Lampiran()
        //{

        //    PropertyInfo[] Props = typeof(Lampiran).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    if (dtLampiran.Columns.Count <= 0)
        //    {
        //        foreach (PropertyInfo prop in Props)
        //        {
        //            dtLampiran.Columns.Add(prop.Name);
        //        }
        //    }

        //    for (int i = 0; i < Request.Files.Count; i++)
        //    {
        //        HttpPostedFileBase file = Request.Files[i];
        //        int fileSize = file.ContentLength;
        //        string mimeType = file.ContentType;
        //        System.IO.Stream fileContent = file.InputStream;
        //        string filePath = Path.Combine(@"\\b7-drive.bintang7.com\Intranetportal\Intranet Attachment\QS\CAPA\Koordinator\", Path.GetFileName(file.FileName));
        //        DataRow rowstype = dtLampiran.NewRow();
        //        rowstype["LAMPIRAN_TERKAIT"] = filePath;
        //        rowstype["FILE_NAME"] = file.FileName;
        //        dtLampiran.Rows.Add(rowstype);

        //        //string filePath = Path.Combine(@"\\b7-drive.bintang7.com\Intranetportal\Intranet Attachment\QS\CAPA\Koordinator\", Path.GetFileName(file.FileName));
        //        //string filePath = Path.Combine(Server.MapPath("~/Content/Files/"), Path.GetFileName(file.FileName)); 
        //        //file.SaveAs(filePath);
        //    }
        //    return dtLampiran;
        //}

        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                    //if (values[i].ToString().Contains("&amp;"))
                    //{
                    //    values[i].ToString().Replace("&amp;", "&");
                    //}
                }

                dt.Rows.Add(values);
            }
            return dt;
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
        #endregion

        #region Koordinator Verifikasi

        public ActionResult VerifikasiPelaksanaCAPA(string NoCAPA)
        {
            return View();
        }

        public ActionResult GetVerfifikasiData(DALModel Model)
        {
            Model.SP = "[dbo].[SP_VERIFIKASI_PELAKSANA_CAPA]";
            Model.Option = 1;

            return Json(DAL.GetDataPrint(Model));
        }

        public ActionResult VerifyPelaksanaCAPA(DALModel Model)
        {
            var dictionary = new Dictionary<string, object>();
            if (Model.Type == "Perbaikan")
            {

                dictionary.Add("Option", 2);
                dictionary.Add("Create_By", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("RecordID", Model.RecordID);

            }
            else
            if (Model.Type == "Pencegahan")
            {
                dictionary.Add("Option", 3);
                dictionary.Add("Create_By", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("RecordID", Model.RecordID);
            }
            else
            {
                dictionary.Add("Option", 4);
                dictionary.Add("Create_By", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("RecordID", Model.RecordID);
            }

            var spname = "SP_VERIFIKASI_PELAKSANA_CAPA";

            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));
        }

        public ActionResult RejectPelaksanaCAPA(DALModel Model)
        {

            var dictionary = new Dictionary<string, object>();
            if (Model.Type == "Perbaikan")
            {

                dictionary.Add("Option", 5);
                dictionary.Add("Create_By", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("RecordID", Model.RecordID);
                dictionary.Add("AlasanReject", Model.AlasanReject);
            }
            else
            if (Model.Type == "Pencegahan")
            {
                dictionary.Add("Option", 6);
                dictionary.Add("Create_By", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("RecordID", Model.RecordID);
                dictionary.Add("AlasanReject", Model.AlasanReject);
            }
            else
            {
                dictionary.Add("Option", 7);
                dictionary.Add("Create_By", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("RecordID", Model.RecordID);
                dictionary.Add("AlasanReject", Model.AlasanReject);
            }
            var spname = "SP_VERIFIKASI_PELAKSANA_CAPA";

            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));
        }

        public ActionResult CheckStatus(DALModel Model)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Option", 8 },
                {"NO_CAPA", Model.NO_CAPA }
            };

            var spname = "SP_VERIFIKASI_PELAKSANA_CAPA";
            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, spname));
        }       
        #endregion
    }
}