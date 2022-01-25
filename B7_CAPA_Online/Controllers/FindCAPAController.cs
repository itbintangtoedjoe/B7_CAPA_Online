using B7_CAPA_Online.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace B7_CAPA_Online.Controllers
{
    public class FindCAPAController : Controller
    {
        // GET: FindCAPA
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["CAPAONLINE"];
        readonly DataTable DT = new DataTable();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindCAPA()
        {
            return View();
        }

        public ActionResult Reporting()
        {
            return View();
        }

        public ActionResult LoadDataReporting()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SP_SHOW_REPORTING", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows, JsonRequestBehavior.AllowGet);
        }
    }
}