using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using B7_CAPA_Online.Scripts.SMTP;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public ActionResult SubmitApproval(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Recipient = DAL.StoredProcedure(parameters, "[dbo].[SP_EMAIL]");

            var list = new List<Recipients>();
            var objects = JsonConvert.DeserializeObject(Recipient.ToString()); // parse as array  

            foreach (var item in ((JArray)objects))
            {
                list.Add(new Recipients
                {
                    NO_CAPA = item.Value<string>("NoCAPA")
                        ,
                    Email = item.Value<string>("Email")
                        ,
                    KategoriCAPA = item.Value<string>("KategoriCAPA")
                        ,
                    ToEmpName = item.Value<string>("ToEmpName")
                        ,
                    TriggerCAPA = item.Value<string>("TriggerCAPA")
                        ,
                    Lokasi = item.Value<string>("Lokasi")
                        ,
                    StatusCAPA = item.Value<string>("StatusCAPA")
                        ,
                    PIC = item.Value<string>("PIC")
                        ,
                    RejectReason = item.Value<string>("RejectReason")
                        ,
                    DeskripsiMasalah = item.Value<string>("DeskripsiMasalah")
                        ,
                    CreateBy = item.Value<string>("Requestor")
                });
            }

            if (list[0].NO_CAPA != null)
            {
                EmailSender emailSender = new EmailSender();
                foreach (var value in list)
                {
                    emailSender.SendEmail(new Dictionary<string, object> {
                        {"Nama_Aplikasi", "CAPA" }, // ini hardcode 
                        {"Kategori", "PICReminder" }, // ini hardcode
                        {"KategoriCAPA", value.KategoriCAPA }, // dari list diatas
                        {"ToEmpName", value.ToEmpName }, // dari list diatas
                        {"Recipient", Recipient}, // recipient object list dari atas
                        {"TriggerCAPA", value.TriggerCAPA}, // ini dari list diatas
                        {"Lokasi", value.Lokasi}, // dari list diatas
                        {"StatusCAPA", value.StatusCAPA}, // ini dari list diatas
                        {"RejectReason", value.RejectReason}, // ini dari list diatas
                        {"PIC", value.PIC}, // ini dari list diatas
                        {"CreateBy", value.CreateBy}, // ini dari list diatas
                        {"DeskripsiMasalah", value.DeskripsiMasalah } // ini dari list diatas
                    });
                }
            }
            return Json(Recipient);}
        }
}