using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    public class PendingTaskController : Controller
    {
        // GET: PendingTask
        DataAccess DAL = new DataAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskList()
        {
            return View();
        }

        public ActionResult GetTaskList(DALModel Model)
        {            
            return Json(DAL.GetDataPrint(Model));
        }
    }
}