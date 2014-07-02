using System.Web.Mvc;

namespace PaulaPires.Areas.administrador
{
    public class administradorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "administrador";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "administrador_default",
                "administrador/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
