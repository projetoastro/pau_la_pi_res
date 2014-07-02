using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaulaPires.Areas.administrador.Filters
{
    public class Validacoes : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authroized = base.AuthorizeCore(httpContext);
            if (!authroized)
            {
                return false;
            }

            var session = httpContext.Session["user"];
            if (session == null)
            {
                return false;
            }

            return true;
        }
    }
}