﻿using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
