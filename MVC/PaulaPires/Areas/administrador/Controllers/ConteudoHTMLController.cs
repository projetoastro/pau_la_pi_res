using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Areas.administrador.Models;
using PaulaPires.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class ConteudoHTMLController : Controller
    {       
        public ActionResult Cadastrar(int pPaginaId)
        {
            var modeloHTML = new ModeloHtml();

            ViewBag.SucessoForm = null;

            modeloHTML.LoadbyPagina(pPaginaId);
            
            List<Paginas> paginas = new List<Paginas>();

            foreach (var item in Paginas.ListByModelo(1))
            {
                Paginas pagina = new Paginas
                {
                    Id = item.Id,
                    TituloPagina = string.Format("{0} - [{1}]", item.TituloPagina, item.TituloMenu)
                };
                paginas.Add(pagina);
            }

            ViewBag.Paginas = paginas;

            return View(modeloHTML);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Cadastrar(ModeloHtml modeloHtml, HttpPostedFileBase thumb)
        {
            if (SuperController.RetornaCheckbox(modeloHtml.RemoveThumb))
            {
                modeloHtml.Imagem = string.Empty;
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
                    if (modeloHtml.Id > 0)
                    {
                        //var modeloHtmlImagem = new ModeloHtml(modeloHtml.Id);
                        thumbUpload = modeloHtml.Imagem;
                    }
                }

                modeloHtml.Imagem = thumbUpload;
            }

            bool result = modeloHtml.Save();

            if (result)
            {
                return RedirectToAction("Index", "Paginas", new {sucessoForm = true});
            }

            return RedirectToAction("Cadastrar", new {sucessoForm = true});
        }

        private string EnvioImagemUpload(HttpPostedFileBase photoFile)
        {
            string[] extensionParts = Path.GetFileName(photoFile.FileName).Split('.');
            string extension = extensionParts[extensionParts.Length - 1].ToLower();
            string file_name = string.Format(String.Format("{0}.{1}", System.Guid.NewGuid().ToString(), extension));

            var path = Path.Combine(Server.MapPath("~/img/paginasHTML/"), file_name);

            photoFile.SaveAs(path);

            UploadImageHelper.resizeImage(555, 400, path);

            return file_name;
        }
    }
}
