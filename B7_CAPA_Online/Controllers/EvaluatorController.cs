using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    public class EvaluatorController : Controller
    {
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
    }
}