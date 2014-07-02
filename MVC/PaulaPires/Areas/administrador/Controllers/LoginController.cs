using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PaulaPires.Areas.administrador.Models;

namespace PaulaPires.Areas.administrador.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /administrador/Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, string ReturnUrl)
        {
            ViewBag.Error = false;
            // Efetua o login
            var usuario = new Usuarios(username, password);

            if (usuario.Id > 0)
            {
                Session["user"] = usuario;
                FormsAuthentication.SetAuthCookie(username, false);

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    ReturnUrl = "/administrador";
                }

                return Redirect(ReturnUrl);   
            }

            ViewBag.Error = true;
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
