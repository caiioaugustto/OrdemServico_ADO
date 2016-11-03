using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uteis;

namespace Controllers
{
    public class LoginController : Controller
    {
        LoginRepository loginRepo = new LoginRepository();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logar(String usuario, String senha)
        {
            Login login = loginRepo.Buscar(usuario, senha);

            if (login.Usuario != null && login.Senha != null)
            {
                SessionManager.UsuarioLogado = login;
                System.Web.Security.FormsAuthentication.SetAuthCookie(login.Usuario, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("login.Invalido", "Usuário ou senha Inválido");
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return View("Index");
        }
    }
}