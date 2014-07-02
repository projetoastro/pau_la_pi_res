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
    public class ConteudoVideosController : Controller
    {
        public ActionResult Cadastrar(int pPaginaId)
        {
            var modeloVideo = new ModeloVideos();

            ViewBag.SucessoForm = null;

            modeloVideo.LoadbyPagina(pPaginaId);
            ViewBag.Paginas = Paginas.ListByModelo(2);

            return View(modeloVideo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloVideos modeloVideos)
        {
            bool result = modeloVideos.Save();

            if (result)
            {
                return RedirectToAction("Index", "Paginas", new { sucessoForm = true });
            }

            return RedirectToAction("Cadastrar", new { sucessoForm = true });
        }
    }
}
