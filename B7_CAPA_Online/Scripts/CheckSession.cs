using B7_CAPA_Online.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace B7_CAPA_Online.Scripts
{
    public class CheckSession : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["Username"] == null)
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                //filterContext.Result = new JsonResult { Data = "LogOut" };
            }
            base.OnActionExecuting(filterContext);
        }
    }
}