using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class ConteudoMidiasImagensController : Controller
    {
        public ActionResult Cadastrar(int pCadastro = 0)
        {
            var modelo = new ModeloFAQ();

            ViewBag.SucessoForm = null;

            if (pCadastro > 0)
            {
                modelo.Load(pCadastro);
            }

            ViewBag.Paginas = Paginas.ListByModelo(5);

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloMidiasImagens modelo)
        {
            bool result = modelo.Save();

            if (result)
            {
                return RedirectToAction("Index", "Paginas", new { sucessoForm = true });
            }

            return RedirectToAction("Cadastrar", new { sucessoForm = true });
        }

        [HttpPost]
        public ActionResult Deletar(int pCadastro)
        {
            var cadastro = new ModeloFAQ()
            {
                Id = pCadastro
            };
            var result = cadastro.Deletar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
