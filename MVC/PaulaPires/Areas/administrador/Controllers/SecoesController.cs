using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class SecoesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.secoes = Secoes.List();
            return View();
        }
    }
}
