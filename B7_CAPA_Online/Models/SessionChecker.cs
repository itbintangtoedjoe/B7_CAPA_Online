using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class SessionChecker
    {
        public bool isAuth { get; set; }
        public bool Checker()
        {
            if (HttpContext.Current.Session.Count > 0)
            {
                this.isAuth = true;
            }
            return isAuth;
        }
    }
}