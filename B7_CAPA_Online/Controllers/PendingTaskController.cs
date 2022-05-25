using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    [CheckSession]
    public class PendingTaskController : Controller
    {
        // GET: PendingTask
        readonly DataAccess DAL = new DataAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskList()
        {
            return View();
        }

        public ActionResult GetTaskList(PendingTaskModel Model)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"NIK", Model.NIK}
            };
            var parameters = new DynamicParameters(dictionary);
            return Json(DAL.StoredProcedure(parameters, "SP_PENDING_TASK"));
        }
    }
}