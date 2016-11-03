using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uteis;

namespace Controllers
{
    [AutorizacaoFilter]
    public class FornecedorController : Controller
    {
        FornecedorRepository fornRepo = new FornecedorRepository();

        // GET: Fornecedor
        public ActionResult Index()
        {
            IList<Fornecedor> listarFornecedores = fornRepo.Listar();
            return View(listarFornecedores);
        }

        public ActionResult PreencherCadastro()
        {
            return View();
        }

        public ActionResult Cadastrar(Fornecedor fornecedor)
        {
            fornRepo.Cadastrar(fornecedor);
            IList<Fornecedor> listarFornecedores = fornRepo.Listar();
            return View("Index", listarFornecedores);
        }
    }
}