using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;
namespace B7_CAPA_Online.Controllers
{
    public class ReportCAPAController : Controller
    {
        // GET: ReportCAPA
        readonly DataAccess DAL = new DataAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportMasterlist()
        {
            return View();
        }

        public ActionResult ReportLeadtime()
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