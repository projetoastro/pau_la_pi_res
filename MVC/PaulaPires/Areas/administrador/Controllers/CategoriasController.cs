using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Areas.administrador.Models;
using PaulaPires.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class CategoriasController : Controller
    {
        public ActionResult Index(bool sucessoForm = false)
        { 
            ViewBag.SucessoForm = sucessoForm;

            ViewBag.categorias = Categorias.List();
            return View();
        }

        public ActionResult Cadastrar(int pCadastro = 0)
        {
            var categoria = new Categorias();

            ViewBag.SucessoForm = null;
            ViewBag.Secoes = Secoes.List();

            if (pCadastro > 0)
            {
                categoria.Load(pCadastro);
            }

            return View(categoria);
        }

        [HttpPost]
        public ActionResult Cadastrar(Categorias categoria, HttpPostedFileBase thumb)
        {
            var categoriaObj = new Categorias {SecaoId = {Id = int.Parse(categoria.SecaoIdValue)}};

            if (SuperController.RetornaCheckbox(categoria.RemoveThumb))
            {
                categoria.Thumb = string.Empty;
            }
            else
            {
                string thumbUpload = string.Empty;
                if (thumb != null)
                {
                    thumbUpload = EnvioImagemUpload(thumb);
                }
                else
                {
                    // caso o thumb não seja selecionado mantém o atual
                    if (categoria.Id > 0)
                    {
                        var categoriaThumb = new Categorias(categoria.Id);
                        thumbUpload = categoriaThumb.Thumb;
                    }
                }

                categoria.Thumb = thumbUpload;
            }
            categoria.SecaoId.Id = categoriaObj.SecaoId.Id;
            bool result = categoria.Save();

            if (result)
            {
                return RedirectToAction("Index", new { sucessoForm = true});
            }
            
            ViewBag.ErrorForm = true;
            return View();
        }

        private string EnvioImagemUpload(HttpPostedFileBase photoFile)
        {
            string[] extensionParts = Path.GetFileName(photoFile.FileName).Split('.');
            string extension = extensionParts[extensionParts.Length - 1].ToLower();
            string file_name = string.Format(String.Format("{0}.{1}", System.Guid.NewGuid().ToString(), extension));

            var path = Path.Combine(Server.MapPath("~/img/tecnicas/"), file_name);

            photoFile.SaveAs(path);

            UploadImageHelper.resizeImage(58, 49, path);

            return file_name;
        }

        public ActionResult Visualizar(int pCategoria)
        {
            var categorias = new Categorias();
            categorias.Load(pCategoria);

            ViewBag.SubCategorias = SubCategorias.ListSubcategorias(categorias.Id);

            return View(categorias);
        }

        public ActionResult DesabilitarHabilitar(int pCategoria)
        {
            var categorias = new Categorias();
            categorias.DesabilitarHabilitar(pCategoria);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Deletar(int pCategoria)
        {
            var categorias = new Categorias
            {
                Id = pCategoria
            };            
            var result = categorias.Deletar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ValidaOrdem(int pSecaoId, int pCategoriaId, int pOrderBy)
        {
            var categoria = new Categorias();
            bool result = categoria.ValidaOrdemCategoria(pCategoriaId, pSecaoId, pOrderBy);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
