using B7_CAPA_Online.Models;
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
    public class EvaluatorController : Controller
    {
        DataAccess DAL = new DataAccess();
        // GET: Evaluator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApprovalEvaluator()
        {
            return View();
        }

        public ActionResult VerifikasiCAPA()
        {
            return View();
        }

        public ActionResult Evaluator()
        {
            return View();
        }

        public ActionResult EvaluatorCancelCAPA()
        {
            return View();
        }

        public ActionResult EvaluatorUpdateCAPA()
        {
            return View();
        }

        public ActionResult CancelCAPA()
        {
            return View();
        }

        public ActionResult ReviewCAPA(string NoCAPA)
        {
            return View();
        }

        public ActionResult DynamicParameter(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname));

        }
    }
}