using B7_CAPA_Online.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    public class PICController : Controller
    {
        public static DataTable DT = new DataTable();
        // GET: PIC
        //DataTable DT = new DataTable();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PICCAPA()
        {
            var list = new ListKondisi();
            list.ClearDT();
            return View();
        }

        public ActionResult AddKondisi(AnalisaKondisiModel Model)
        {
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<AnalisaKondisiModel> newList = new List<AnalisaKondisiModel>();
            newList.Add(Model);
            var list = new ListKondisi();
            var result = list.ToDataTable<AnalisaKondisiModel>(newList);                    
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

        //public ActionResult AddPartial()
        //{            
        //    return PartialView("SaverityDDL");
        //}

    }
}