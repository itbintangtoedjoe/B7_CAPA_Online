using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public ActionResult AddDepartments(ListDepartemenModel Model)
        {            
            try
            {
                int count = 0;
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
                Model.LampiranTerkait = filePath;
                Model.SP = "[dbo].[SP_CAPA_ID]";
                file.SaveAs(filePath);
            }
            string jsonObj = string.Join(",", Model.DepartemenCollection.ToArray());            
            dynamic deptList = JsonConvert.DeserializeObject<List<Dept>>(jsonObj);

            string penyimpanganObj = string.Join(",", Model.PenyimpanganCollection.ToArray());
            dynamic penyimpanganList = JsonConvert.DeserializeObject<List<Penyimpangan>>(penyimpanganObj);

            DataTable dt = new DataTable();            
            dt.Columns.Add("DEPARTEMEN");
            foreach (var str in deptList)
            {
                var val = str.GetType()
                .GetProperty("Departemen")
                .GetValue(str);
                DataRow rowstype = dt.NewRow();
                rowstype["DEPARTEMEN"] = val;
                dt.Rows.Add(rowstype);
            }
            DataTable dt_penyimpangan = new DataTable();
            if (penyimpanganList != null)
            {               
                dt_penyimpangan.Columns.Add("PENYIMPANGAN_ID");
                foreach (var str in penyimpanganList)
                {
                    var val = str.GetType()
                    .GetProperty("PENYIMPANGAN_ID")
                    .GetValue(str);
                    DataRow rowstype = dt_penyimpangan.NewRow();
                    rowstype["PENYIMPANGAN_ID"] = val;
                    dt_penyimpangan.Rows.Add(rowstype);
                }
            }
            DAL.InsertData(Model, dt, dt_penyimpangan);
            return Json("test");
        }
        #endregion
    }
}