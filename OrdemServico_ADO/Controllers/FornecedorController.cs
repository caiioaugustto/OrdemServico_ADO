using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controllers
{
    public class FornecedorController : Controller
    {
        FornecedorRepository fornRepo = new FornecedorRepository();

        // GET: Fornecedor
        public ActionResult Index()
        {
            if(Uteis.SessionManager.IsAuthenticated)
            {
                IList<Fornecedor> listarFornecedores = fornRepo.Listar();
                return View(listarFornecedores);
            }else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }

        public ActionResult PreencherCadastro()
        {
            if (Uteis.SessionManager.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Cadastrar(Fornecedor fornecedor)
        {
            if(Uteis.SessionManager.IsAuthenticated)
            {
                fornRepo.Cadastrar(fornecedor);

                IList<Fornecedor> listarFornecedores = fornRepo.Listar();

                return View("Index", listarFornecedores);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }
    }
}