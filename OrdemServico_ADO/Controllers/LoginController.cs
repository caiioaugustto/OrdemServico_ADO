using Entidades;
using Repository;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web.Mvc;
using Uteis;

namespace Controllers
{
    public class LoginController : Controller
    {
        private LoginRepository loginRepo;
       
        public LoginController(LoginRepository loginRepo)
        {
            this.loginRepo = loginRepo;
        }
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logar(string usuario, string senha)
        {
            var senhaCripto = Criptografia.CriptografaMd5(senha);

            Login login = loginRepo.Buscar(usuario, senhaCripto);

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