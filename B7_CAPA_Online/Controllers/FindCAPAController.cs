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

        

        
    }
}