using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
    public class PICController : Controller
    {
        public static DataTable DT = new DataTable();
        // GET: PIC               
        DataAccess DAL = new DataAccess();

        #region View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FormCAPA()
        {
            var list = new ListKondisi();
            list.ClearDT();
            return View();
        }

        #endregion

        #region Populate
        public ActionResult GetDataPIC(AnalisaKondisiModel Model)
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause}
            };
            string Result = DAL.GetDataFormPIC(Model,dictionary);
            return Json(Result);
        }

        public ActionResult GetKajianResiko(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }
        #endregion

        #region Execute
        public ActionResult AddKondisi(AnalisaKondisiModel Model)
        {
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<AnalisaKondisiModel> newList = new List<AnalisaKondisiModel>();
            newList.Add(Model);
            var list = new ListKondisi();
            //var result = list.ToDataTable<AnalisaKondisiModel>(newList); // Insert to DataTable
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause}
            };
            string Return = DAL.ExecuteFormPIC(Model,dictionary); // Insert to WAHHeader 

            //foreach (DataRow dr in result.Rows)
            //{
            //    row = new Dictionary<string, object>();
            //    foreach (DataColumn col in result.Columns)
            //    {
            //        row.Add(col.ColumnName, dr[col]);                   
            //    }
            //    rows.Add(row);
            //}
            return Json(Return);
        }

        public ActionResult AddWhy(AnalisaKondisiModel Model)
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause}
            };
            string Return = DAL.ExecuteFormPIC(Model,dictionary);
            return Json(Return);
        }

        public ActionResult DeleteKondisi(AnalisaKondisiModel Model)
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause}
            };
            string Return = DAL.ExecuteFormPIC(Model,dictionary);
            return Json(Return);
        }

        public ActionResult AddTindakan(AnalisaKondisiModel Model)
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", Model.Option },
                { "NO_CAPA",Model.NO_CAPA },
                { "Aspect", Model.Aspect },
                { "WSBH" , Model.WSBH },
                { "WAH" , Model.WAH },
                { "Status" , Model.Status },
                { "isParent" , Model.isParent },
                { "WHY_Parent" , Model.WHY_Parent },
                { "WHY" , Model.WHY },
                { "Tindakan", Model.Tindakan},
                { "Pelaksana", Model.Pelaksana},
                { "LineNumber", Model.LineNumber},
                { "NamaPersonil", Model.NamaPersonil},
                { "Email", Model.Email},
                { "DueDate", Model.DueDate},
                { "Is_AreaLain", Model.Is_AreaLain},
                { "WhyID", Model.WhyID},
                { "WhyParentID", Model.WhyParentID},
                { "RootCause", Model.RootCause}
            };
            
            string Return = DAL.ExecuteFormPIC(Model,dictionary);
            return Json(Return);
        }
        public ActionResult AddKajian(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_INSERT_KAJIAN_RESIKO]");
            return Json(Return);
        }
        public ActionResult SubmitAtasanPIC(DynamicModel Param)
        {
            DynamicParameters parameters = new DynamicParameters(Param.Model);
            string Return = DAL.StoredProcedure(parameters, "[dbo].[SP_FORM_CAPA]");
            return Json(Return);
        }

        #endregion


    }
}