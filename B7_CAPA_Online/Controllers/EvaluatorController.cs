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
        #region View
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

        public ActionResult KonfirmasiEfektivitas()
        {
            return View();
        }

        public ActionResult ViewBukti()
        {
            return View();
        }

        public ActionResult ViewCAPA()
        {
            return View();
        }

        public ActionResult ReviewCAPA(string NoCAPA)
        {
            return View();
        }
        #endregion

        #region Populate
        public ActionResult GetData(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_KONFIRMASI_EFEKTIVITAS]");
            return Json(Return);
        }
        #endregion

    }
}