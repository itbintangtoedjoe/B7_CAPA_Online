using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    public class ApprovalEvaluatorController : Controller
    {
        // GET: ApprovalEvaluator
        public ActionResult Index()
        {
            string halo = "";
            return View();
        }

        public ActionResult ApprovalEvaluator()
        {
            return View();
        }
    }
}