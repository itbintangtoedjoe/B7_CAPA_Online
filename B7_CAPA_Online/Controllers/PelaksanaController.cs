using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    public class PelaksanaController : Controller
    {
        readonly DataAccess DAL = new DataAccess();
        // GET: Pelaksana
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PelaksanaCAPA()
        {
            return View();
        }
        public ActionResult FindCAPADetail(SPFindDataParams data)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            var dictionary = new Dictionary<string, object>{
                { "kategori", data.Kategori },
                { "nomorCAPA", data.NomorCAPA },
                { "nik", data.NIK },
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Find_Data");
            return Json(result);
        }
    }
}