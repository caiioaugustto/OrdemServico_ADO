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
    public class OrdemServicoController : Controller
    {
        OrdemServicoRepository OrdemRepo = new OrdemServicoRepository();
        FornecedorRepository fornRepo = new FornecedorRepository();

        // GET: OrdemServico
        public ActionResult Index()
        {
            IList<OrdemServico> listarOS = OrdemRepo.Listar();
            return View(listarOS);
        }

        public ActionResult PreencherOrdemServico()
        {
            ViewBag.Fornecedor = fornRepo.Listar();
            return View();
        }

        public ActionResult Cadastrar(OrdemServico ordemServico)
        {
            OrdemRepo.Cadastrar(ordemServico);
            return RedirectToAction("Index", "OrdemServico");
        }

        //public void Editar(int id)
        //{
        //    OrdemRepo.Editar(id);
        //}

        public void Excluir(int id)
        {
            OrdemRepo.Exclur(id);
        }
    }
}