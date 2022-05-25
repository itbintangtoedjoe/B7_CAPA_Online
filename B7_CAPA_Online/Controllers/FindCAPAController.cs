using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    [CheckSession]
    public class FindCAPAController : Controller
    {
        // GET: FindCAPA
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["CAPAONLINE"];
        readonly DataTable DT = new DataTable();
        DataAccess DAL = new DataAccess();
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


        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname));

        }
    }
}