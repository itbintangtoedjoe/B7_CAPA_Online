using B7_CAPA_Online.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
                //filterContext.HttpContext.Response.StatusCode = 403;
                //filterContext.Result = new JsonResult { Data = "LogOut" };
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                }
                //if (filterContext.HttpContext.Request.IsAjaxRequest())
                //{
                //    var model = new
                //    {
                //        AjaxFailed = "true",
                //        RedirectURL = "localhost/home/index"
                //    };
                //    //filterContext.Result = new JsonResult { Data = "LogOut" };
                //}
                else
                {
                    //filterContext.Result = new RedirectResult("~/Login/Session_Error");
                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary {
                                    { "Controller", "Login" },
                                    { "Action", "Session_Error" }
                                   });
                }
                //filterContext.Result = new RedirectToRouteResult(
                //   new RouteValueDictionary {
                //                { "Controller", "Login" },
                //                { "Action", "Session_Error" }
                //               });
                base.OnActionExecuting(filterContext);
            }
        }
    }
}