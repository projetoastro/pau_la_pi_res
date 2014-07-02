using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaulaPires
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Carrega dados",
                url: "{categoria}/{subcategoria}/{pagina}",
                defaults: new { controller = "Home", action = "MostraConteudo" },
                namespaces: new[] { "PaulaPires.Controllers" }
            );

            routes.MapRoute(
                name: "Carrega dados 2",
                url: "{categoria}/{pagina}",
                defaults: new { controller = "Home", action = "MostraConteudoCategoria" },
                namespaces: new[] { "PaulaPires.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PaulaPires.Controllers" }
            );
        }
    }
}