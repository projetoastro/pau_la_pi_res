using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaulaPires.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    public class SignosController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.signos = Signos.List();
            return View();
        }

    }
}
