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
    public class AtasanPICController : Controller
    {
        // GET: AtasanPIC
        DataAccess DAL = new DataAccess();

        #region View
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AtasanPIC()
        {
            return View();
        }

        public ActionResult ApprovalAtasanPIC()
        {
            return View();
        }
        #endregion

        #region Populate

        #endregion

        #region Execute
        public ActionResult SubmitApproval(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_APPROVAL_CAPA]");
            return Json(Return);
        }
        #endregion
    }
}