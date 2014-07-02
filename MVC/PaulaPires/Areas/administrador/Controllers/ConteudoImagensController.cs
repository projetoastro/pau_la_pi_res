using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Areas.administrador.Models;
using PaulaPires.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class ConteudoImagensController : Controller
    {
        public ActionResult Cadastrar(int pCadastro = 0)
        {
            var modelo = new ModeloImagens();

            ViewBag.SucessoForm = null;

            if (pCadastro > 0)
            {
                modelo.Load(pCadastro);
            }

            ViewBag.Paginas = Paginas.ListByModelo(12);

            return View(modelo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Cadastrar(ModeloImagens modelo, HttpPostedFileBase thumb)
        {
            string thumbUpload = string.Empty;

            thumbUpload = EnvioImagemUpload(thumb);

            modelo.Imagem = thumbUpload;

            bool result = modelo.Save();

            if (result)
            {
                return RedirectToAction("Index", "Paginas", new { sucessoForm = true });
            }

            return RedirectToAction("Cadastrar", new { sucessoForm = true });
        }

        private string EnvioImagemUpload(HttpPostedFileBase photoFile)
        {
            string[] extensionParts = Path.GetFileName(photoFile.FileName).Split('.');
            string extension = extensionParts[extensionParts.Length - 1].ToLower();
            string file_name = string.Format(String.Format("{0}.{1}", System.Guid.NewGuid().ToString(), extension));

            var path = Path.Combine(Server.MapPath("~/img/paginasImagens/"), file_name);
            photoFile.SaveAs(path);
            UploadImageHelper.resizeImage(555, 400, path);

            var pathThumb = Path.Combine(Server.MapPath("~/img/paginasImagens/thumb/"), file_name);
            photoFile.SaveAs(pathThumb);
            UploadImageHelper.resizeImage(50, 40, pathThumb);

            return file_name;
        }

        [HttpPost]
        public ActionResult Deletar(int pCadastro)
        {
            var cadastro = new ModeloImagens()
            {
                Id = pCadastro
            };
            var result = cadastro.Deletar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
