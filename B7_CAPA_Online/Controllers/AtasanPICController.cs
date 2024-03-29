﻿using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using B7_CAPA_Online.Scripts.SMTP;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    [CheckSession]
    public class AtasanPICController : Controller
    {
        // GET: AtasanPIC
        DataAccess DAL = new DataAccess();
        SessionChecker checker = new SessionChecker();

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
            if (!checker.Checker())
            {
                return Redirect("../Login");
            }
            return View();
        }
        #endregion

        #region Populate

        #endregion

        #region Execute
        public ActionResult SubmitApproval(DynamicModel Param)
        {            
            if (!string.IsNullOrEmpty(Session["NIK"] as string))
            {
                DynamicParameters parameters = new DynamicParameters(Param.Model);

                string Recipient = DAL.StoredProcedure(parameters, "[dbo].[SP_APPROVAL_CAPA]");

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
                int values = int.Parse(Param.Model["Option"].ToString());
                bool isValid = true;

                if (values > 6 && values < 10 && values != 8 && list.Count == 0)
                {
                    isValid = false;
                }
                
                if (isValid) // ketika option sp != 7 atau 9 kalau 7 itu untuk approval ReviewCAPA 
                {
                    if (list[0].NO_CAPA != null) // kirim email setiap status header berubah
                    {
                        EmailSender emailSender = new EmailSender();
                        int index = 0;
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
                            }, index);
                            index++;
                        }
                    }                    
                    
                }
                return Json(Recipient);
            }

            return Json("unauthorized");

        }
        #endregion
    }
}