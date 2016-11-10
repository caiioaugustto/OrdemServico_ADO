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

        public ActionResult Editar(int id)
        {
            var fornecedor = fornRepo.PegarFornecedor(id);
            return View(fornecedor);
        }

        public ActionResult SalvarEdicao(Fornecedor fornecedor)
        {
            fornRepo.Editar(fornecedor);
            return RedirectToAction("Index", "Fornecedor");
        }

        public ActionResult Excluir(int id)
        {
            fornRepo.Excluir(id);
            return RedirectToAction("Index", "Fornecedor");
        }
    }
}