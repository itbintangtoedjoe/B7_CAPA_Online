using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{

    public class KoordinatorController : Controller
    {
        // GET: Koordinator
        //readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["CAPAONLINE"];
        //readonly DataTable DT = new DataTable();
        //SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //string Result;
        public DataTable DT = new DataTable();
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

        public ActionResult ApprovalKoordinator2(string NoCAPA)
        {
            ViewBag.NoCAPA = NoCAPA;
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

        public ActionResult CreateCAPA()
        {
            var list = new ListDepartemen();
            list.ClearDT();           
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

        public ActionResult RequestCAPA(string CAPA,string REG)
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
            Model.Departemen = "IT";
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

        public ActionResult GetEvaluator(string departemen, int Option, string Lokasi,string NoCAPA)
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

        public ActionResult GetRoot(string NoCapa,string Type)
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
            var parameters = new DynamicParameters();
            parameters.Add("InsertPenyimpangan", dt.AsTableValuedParameter("[dbo].[InsertPenyimpangan]"));

            var spname = "SP_Insert_Penyimpangan";

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
                {"Departemen","IT" },
                {"JenisKeluhan",Model.JenisKeluhan },
                {"Plant",Model.Plant},
                {"Tahun",Model.Tahun}

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
            catch(Exception e)
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
        public ActionResult GetCAPA(DALModel Model){
            FindCAPAModel findModel = new FindCAPAModel();
            string Result;
            int Count = 0;
            List<ListDepartemenModel> arrListDept = new List<ListDepartemenModel>();
            var list = new ListDepartemen();
            string[] attr = { "DEPARTEMEN", "PENYIMPANGAN", "FILE_PATH" };           
            Result = DAL.GetDataPrint(Model);            
            var CAPA = JsonConvert.DeserializeObject<List<FindCAPAModel>>(Result);
            foreach(var str in CAPA)
            {                
                foreach (string prop in attr){
                    var val = str.GetType()
                    .GetProperty(prop)
                    .GetValue(str);
                    string[] value = val.ToString().Split(',');
                    List<string> newList = new List<string>();
                    foreach (string temp in value)
                    {                        
                        newList.Add(temp.Trim());
                        arrListDept.Add(new ListDepartemenModel { Departemen = temp.Trim()});                        
                    }
                    switch(Count)
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
                    }
                    Count++;
                }                 
            }
            return Json(CAPA);
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
        public ActionResult InsertCAPA(DALModel Model)
        {                     
            for (int i = 0; i < Request.Files.Count; i++)
            {  
                HttpPostedFileBase file = Request.Files[i];                                                             
                int fileSize = file.ContentLength;                
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //string filePath = Path.Combine("@"\\kalbox-b7.bintang7.com\Intranetportal\Intranet Attachment\HRCostUpload\", Path.GetFileName(file.FileName));
                string filePath = Path.Combine(Server.MapPath("~/Content/Files/"), Path.GetFileName(file.FileName));
                Model.LampiranTerkait.Add(new Lampiran { LAMPIRAN_TERKAIT = filePath });               
                Model.SP = "[dbo].[SP_CAPA_ID]";
                //file.SaveAs(filePath);
            }
            string jsonObj = string.Join(",", Model.DepartemenCollection.ToArray());            
            dynamic deptList = JsonConvert.DeserializeObject<List<Dept>>(jsonObj);

            string penyimpanganObj = string.Join(",", Model.PenyimpanganCollection.ToArray());
            dynamic penyimpanganList = JsonConvert.DeserializeObject<List<Penyimpangan>>(penyimpanganObj);            

            var Departemen_DT = CreateDT<Dept>(deptList, "DEPARTEMEN", "Departemen");
            var Penyimpangan_DT = new DataTable();
            if (penyimpanganList != null)
            {
                Penyimpangan_DT = CreateDT<Penyimpangan>(penyimpanganList, "PENYIMPANGAN_ID", "PENYIMPANGAN_ID");
            }
            var Path_DT = CreateDT<Lampiran>(Model.LampiranTerkait, "LAMPIRAN_TERKAIT", "LAMPIRAN_TERKAIT");
            // Method Insert Data
            //DAL.InsertData(Model, Departemen_DT, Penyimpangan_DT, Path_DT);            
            return RedirectToAction("TaskList", "PendingTask", new { success = "succeed" });
        }
        public DataTable CreateDT<T>(List<T> items, string columns, string propName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(columns);
            foreach (var str in items)
            {
                if(str != null)
                {
                    var val = str.GetType()
                    .GetProperty(propName)
                    .GetValue(str);
                    DataRow rowstype = dt.NewRow();
                    rowstype[columns] = val;
                    dt.Rows.Add(rowstype);
                }                
            }
            return dt;
        }
        #endregion
    }
}