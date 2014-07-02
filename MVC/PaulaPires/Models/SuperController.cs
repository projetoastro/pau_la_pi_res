using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaulaPires.Models
{
    public class SuperController : Controller
    {
        public HomeViewModel HomeViewModel { get; set; }

        public SuperController()
        {

        }

        [ChildActionOnly]
        public ActionResult LoadCategoriasBySecao(string partialView, int secaoId)
        {
            return PartialView(partialView, Categorias.ListMenuSite(secaoId));
        }

        public static bool RetornaCheckbox(string value)
        {
            return value == "on";
        }

        [ChildActionOnly]
        public ActionResult LoadConteudoByModelo(string partialView, int modeloId)
        {
            var modeloHtml = new ModeloHtml();
            modeloHtml.LoadbyModelo(modeloId);
            return PartialView(partialView, modeloHtml);
        }

        [ChildActionOnly]
        public ActionResult LoadConteudoByPagina(string partialView, int pagina, int categoria, int subcategoria = 0)
        {
            switch (partialView)
            {
                case "_ModeloListaLinks":
                    var modeloLinks = new ModeloLinks();
                    modeloLinks.LoadbyPaginaId(pagina, categoria, subcategoria);
                    return PartialView(partialView, modeloLinks);
                case "_ModeloVideos":
                    var modeloVideo = new ModeloVideos();
                    modeloVideo.LoadbyPaginaId(pagina, categoria, subcategoria);
                    return PartialView(partialView, modeloVideo);
                case "_ModeloAudios":
                    var modeloAudio = new ModeloAudios();
                    modeloAudio.LoadbyPaginaId(pagina, categoria, subcategoria);
                    return PartialView(partialView, modeloAudio);
                case "_ModeloFAQ":
                    var modeloFAQ = new ModeloFAQ();
                    modeloFAQ.LoadbyPaginaId(pagina, categoria, subcategoria);
                    return PartialView(partialView, modeloFAQ);
                case "_ModeloImagens":
                    var modeloImagens = new ModeloImagens();
                    modeloImagens.LoadbyPaginaId(pagina, categoria, subcategoria);
                    return PartialView(partialView, modeloImagens);
                case "_ModeloHome":
                    var modeloHome = new ModeloHtml();
                    modeloHome.LoadbyPaginaHOME();
                    return PartialView(partialView, modeloHome);
                default:
                    var modeloHtml = new ModeloHtml();
                    modeloHtml.LoadbyPaginaId(pagina, categoria, subcategoria);
                    return PartialView(partialView, modeloHtml);
            }
        }
    }
}