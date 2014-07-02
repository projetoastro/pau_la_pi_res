using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Areas.administrador.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
