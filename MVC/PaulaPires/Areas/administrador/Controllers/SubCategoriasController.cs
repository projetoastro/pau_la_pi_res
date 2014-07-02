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
    public class SubCategoriasController : Controller
    {
        public ActionResult Index(bool sucessoForm = false)
        {
            ViewBag.SucessoForm = sucessoForm;
            ViewBag.subcategorias = SubCategorias.List();
            return View();
        }

        public ActionResult Cadastrar(int pCadastro = 0)
        {
            var subcategoria = new SubCategorias();

            ViewBag.SucessoForm = null;
            ViewBag.Categorias = Categorias.List();

            if (pCadastro > 0)
            {
                subcategoria.Load(pCadastro);
            }

            return View(subcategoria);
        }

        [HttpPost]
        public ActionResult Cadastrar(SubCategorias subCategoria)
        {
            var subcategoriaObj = new SubCategorias{ CategoriaId = { Id = int.Parse(subCategoria.CategoriaIdValue) } };

            subCategoria.CategoriaId.Id = subcategoriaObj.CategoriaId.Id;
            bool result = subCategoria.Save();

            if (result)
            {
                return RedirectToAction("Index", new { sucessoForm = true });
            }

            ViewBag.ErrorForm = true;
            return View();
        }

        public ActionResult Visualizar(int pId)
        {
            var subcategorias = new SubCategorias();
            subcategorias.Load(pId);
            return View(subcategorias);
        }

        public ActionResult DesabilitarHabilitar(int pId)
        {
            var subcategorias = new SubCategorias();
            subcategorias.DesabilitarHabilitar(pId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Deletar(int pId)
        {
            var subcategorias = new SubCategorias
            {
                Id = pId
            };
            var result = subcategorias.Deletar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
