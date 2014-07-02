using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaulaPires.Models;

namespace PaulaPires.Controllers
{
    public class HomeController : SuperController
    {
        public ActionResult Index()
        {
            var modeloHtml = new ModeloHtml();
            modeloHtml.LoadbyPagina(1);
            return View(modeloHtml);
        }

        public ActionResult MostraConteudo(string categoria, string pagina, string subcategoria)
        {
            var paginaObj = new Paginas();
            paginaObj.LoadbyUrlAmigavel(pagina, categoria, subcategoria);
            return View(paginaObj);
        }

        public ActionResult MostraConteudoCategoria(string categoria, string pagina)
        {
            var paginaObj = new Paginas();
            paginaObj.LoadbyUrlAmigavel(pagina, categoria, "");
            return View(paginaObj);
        }
    }
}
