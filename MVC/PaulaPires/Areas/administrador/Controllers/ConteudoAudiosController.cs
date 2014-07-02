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
    public class ConteudoAudiosController : Controller
    {
        public ActionResult Cadastrar(int pPaginaId)
        {
            var modelo = new ModeloAudios();

            ViewBag.SucessoForm = null;

            modelo.LoadbyPagina(pPaginaId);
            ViewBag.Paginas = Paginas.ListByModelo(13);

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloAudios modelo)
        {
            bool result = modelo.Save();

            if (result)
            {
                return RedirectToAction("Index", "Paginas", new { sucessoForm = true });
            }

            return RedirectToAction("Cadastrar", new { sucessoForm = true });
        }
    }
}
