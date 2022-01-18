using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    public class AtasanPelaksanaController : Controller
    {
        readonly DataAccess DAL = new DataAccess();
        // GET: AtasanPelaksana
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApprovalAtasanPelaksana()
        {
            return View();
        }

        public ActionResult AtasanPelaksanaAddCAPA()
        {
            return View();
        }

        public ActionResult AtasanPelaksanaCancelCAPA()
        {
            return View();
        }

        public ActionResult AtasanPelaksanaUpdateCAPA()
        {
            return View();
        }

        public ActionResult AtasanPelaksanaApprovalPerubahanTindakan()
        {
            return View();
        }
        public ActionResult AtasanPelaksanaPembatalanTindakan()
        {
            return View();
        }
        public ActionResult AtasanPelaksanaPenambahanTindakan()
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