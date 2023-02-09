using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace B7_CAPA_Online
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region CreateDirectory
            if (ConfigurationManager.AppSettings["UploadPath"].ToString() == "false")
            {
                // Create Directory Upload jika tidak ada di folder local server
                string FilePath = ConfigurationManager.AppSettings["LocalUploadPathLocation"];

                bool exists = System.IO.Directory.Exists(FilePath);

                if (!exists) System.IO.Directory.CreateDirectory(FilePath);
                if (!exists) System.IO.Directory.CreateDirectory(FilePath + "DiagramCAPA");
                if (!exists) System.IO.Directory.CreateDirectory(FilePath + "Koordinator");
                if (!exists) System.IO.Directory.CreateDirectory(FilePath + "Pencegahan");
                if (!exists) System.IO.Directory.CreateDirectory(FilePath + "Perbaikan");
                if (!exists) System.IO.Directory.CreateDirectory(FilePath + "Treatment");
            }
            #endregion
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
