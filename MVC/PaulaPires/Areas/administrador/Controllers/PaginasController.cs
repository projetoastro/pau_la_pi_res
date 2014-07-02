using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PaulaPires.Areas.administrador.Filters;
using PaulaPires.Areas.administrador.Models;
using PaulaPires.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    [Validacoes]
    public class PaginasController : Controller
    {
        public ActionResult Index(bool sucessoForm = false)
        {
            ViewBag.SucessoForm = sucessoForm;
            try
            {
                ViewBag.paginas = Paginas.List();
            }
            catch (Exception ex)
            {
                ViewBag.HomeViewModel = new HomeViewModel { ErrorNumber = Erros.Save(ex) };
            }
            
            return View();
        }

        public ActionResult DesabilitarHabilitar(int pCadastroId)
        {
            var paginas = new Paginas();
            paginas.DesabilitarHabilitar(pCadastroId);
            return RedirectToAction("Index");
        }

        public ActionResult Cadastrar(int pCadastro = 0)
        {
            var pagina = new Paginas();

            ViewBag.SucessoForm = null;
            ViewBag.Modelos = ModelosPaginas.List();
            ViewBag.Subcategorias = SubCategorias.List();

            //ViewBag.SubcategoriasArray = JsonConvert.SerializeObject(SubCategorias.List().Select(x => x.Nome).Aggregate((x, y) => x + "," + y));

            //ViewBag.SubcategoriasArray = string.Join("\", \"", SubCategorias.List().Select(x => x.Nome));

            StringBuilder stbListaSubCategorias = new StringBuilder();

            foreach (var item in SubCategorias.List())
            {
                stbListaSubCategorias.Append("{");
                stbListaSubCategorias.Append(string.Format("id: {0}, text: '{1} [{2}]'", item.Id, item.Nome, item.CategoriaId.Nome));
                stbListaSubCategorias.Append("}");
                stbListaSubCategorias.Append(",");
            }

            StringBuilder stbListaCategorias = new StringBuilder();

            foreach (var item in Categorias.List())
            {
                stbListaCategorias.Append("{");
                stbListaCategorias.Append(string.Format("id: {0}, text: '{1} [{2}]'", item.Id, item.Nome, item.SecaoId.Nome));
                stbListaCategorias.Append("}");
                stbListaCategorias.Append(",");
            }

            //ViewBag.SubcategoriasArray = JsonConvert.SerializeObject(SubCategorias.List().Select(x => x.Nome).ToArray());

            ViewBag.SubcategoriasArray = stbListaSubCategorias.ToString();

            ViewBag.CategoriasArray = stbListaCategorias.ToString();

            if (pCadastro > 0)
            {
                pagina.Load(pCadastro);

                StringBuilder stbListaSubCategoriasSelecionadas = new StringBuilder();
                foreach (var item in CategoriaSubCategoriaPaginas.ListByPagina(pagina.Id))
                {
                    if (!string.IsNullOrEmpty(item.SubCategoria.Nome))
                    {
                        stbListaSubCategoriasSelecionadas.Append("{");
                        stbListaSubCategoriasSelecionadas.Append(string.Format("id: {0}, text: '{1} [{2}]'",
                                                                               item.SubCategoria.Id,
                                                                               item.SubCategoria.Nome, item.SubCategoria.CategoriaId.Nome ));
                        stbListaSubCategoriasSelecionadas.Append("}");
                        stbListaSubCategoriasSelecionadas.Append(",");
                    }
                }
                ViewBag.SubcategoriasSelecionadasArray = stbListaSubCategoriasSelecionadas.ToString();

                //carrega as categorias principais para mostrar na tela
                StringBuilder stbListaCategoriasSelecionadas = new StringBuilder();
                foreach (var item in CategoriaSubCategoriaPaginas.ListByPagina(pagina.Id))
                {
                    if (!string.IsNullOrEmpty(item.Categoria.Nome))
                    {
                        stbListaCategoriasSelecionadas.Append("{");
                        stbListaCategoriasSelecionadas.Append(string.Format("id: {0}, text: '{1} [{2}]'", item.Categoria.Id,
                                                                            item.Categoria.Nome, item.Categoria.SecaoId.Nome));
                        stbListaCategoriasSelecionadas.Append("}");
                        stbListaCategoriasSelecionadas.Append(",");
                    }
                }
                ViewBag.CategoriasSelecionadasArray = stbListaCategoriasSelecionadas.ToString();
            }

            return View(pagina);
        }

        [HttpPost]
        public ActionResult Cadastrar(Paginas pagina, HttpPostedFileBase imagemcompartilha)
        {
            if (SuperController.RetornaCheckbox(pagina.RemoveThumb))
            {
                pagina.ImagemCompartilha = string.Empty;
            }
            else
            {
                string thumbUpload = string.Empty;
                if (imagemcompartilha != null)
                {
                    thumbUpload = EnvioImagemUpload(imagemcompartilha);
                }
                else
                {
                    // caso a imagem não seja selecionada mantém o atual
                    if (pagina.Id > 0)
                    {
                        var categoriaThumb = new Categorias(pagina.Id);
                        thumbUpload = categoriaThumb.Thumb;
                    }
                }

                pagina.ImagemCompartilha = thumbUpload;
            }

            bool result = pagina.Save();

            if (!string.IsNullOrEmpty(pagina.SubCategoriasPagina))
            {
                CadastraSubCategoriasPaginas(pagina.Id, pagina.SubCategoriasPagina);
            }

            if (!string.IsNullOrEmpty(pagina.CategoriasPagina))
            {
                CadastraCategoriasPaginas(pagina.Id, pagina.CategoriasPagina);
            }

            if (result)
            {
                return RedirectToAction("Index", new { sucessoForm = true });
            }

            ViewBag.ErrorForm = true;
            return View();
        }

        private void CadastraSubCategoriasPaginas(int pPaginaId, string subcategoriasPagina)
        {
            string[] arraySubcategoriasPagina = subcategoriasPagina.Split(',');
            var categoriaSubCategoriaPaginas = new CategoriaSubCategoriaPaginas();

            for (int i = 0; i < arraySubcategoriasPagina.Count(); i++)
            {
                categoriaSubCategoriaPaginas.SubCategoriaID = int.Parse(arraySubcategoriasPagina[i]);
                categoriaSubCategoriaPaginas.PaginaID = pPaginaId;
                categoriaSubCategoriaPaginas.SaveByCategoriaSubCategoria();
            }
        }

        private void CadastraCategoriasPaginas(int pPaginaId, string categoriasPagina)
        {
            string[] arrayCategoriasPagina = categoriasPagina.Split(',');
            var categoriaSubCategoriaPaginas = new CategoriaSubCategoriaPaginas();

            for (int i = 0; i < arrayCategoriasPagina.Count(); i++)
            {
                categoriaSubCategoriaPaginas.CategoriaID = int.Parse(arrayCategoriasPagina[i]);
                categoriaSubCategoriaPaginas.PaginaID = pPaginaId;
                categoriaSubCategoriaPaginas.SaveByCategoriaSubCategoria();
            }
        }

        [HttpPost]
        public ActionResult Deletar(int pCadastro)
        {
            var paginas = new Paginas
            {
                Id = pCadastro
            };
            var result = paginas.Deletar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private string EnvioImagemUpload(HttpPostedFileBase photoFile)
        {
            string[] extensionParts = Path.GetFileName(photoFile.FileName).Split('.');
            string extension = extensionParts[extensionParts.Length - 1].ToLower();
            string file_name = string.Format(String.Format("{0}.{1}", System.Guid.NewGuid().ToString(), extension));

            var path = Path.Combine(Server.MapPath("~/img/paginasCompartilha/"), file_name);

            photoFile.SaveAs(path);

            UploadImageHelper.resizeImage(555, 400, path);

            return file_name;
        }

        public ActionResult Visualizar(int pCadastroId)
        {
            var paginas = new Paginas();
            paginas.Load(pCadastroId);

            ViewBag.CategoriasSubCategoriasPaginas = CategoriaSubCategoriaPaginas.ListByPagina(pCadastroId);

            switch (paginas.Modelos.PartialView)
            {
                case "":
                    ViewBag.LinkConteudo = "ConteudoHTML";
                    break;
                default:
                    ViewBag.LinkConteudo = "ConteudoHTML";
                    break;
            }

            return View(paginas);
        }

        public ActionResult LoadListFAQ(int paginaID)
        {
            return PartialView("_ListaFAQ", ModeloFAQ.ListbyPagina(paginaID));
        }

        public ActionResult LoadListLinks(int paginaID)
        {
            return PartialView("_ListaLinks", ModeloLinks.ListbyPagina(paginaID));
        }

        public ActionResult LoadListImagens(int paginaID)
        {
            return PartialView("_ListaImagens", ModeloImagens.ListbyPagina(paginaID));
        }
    }
}


