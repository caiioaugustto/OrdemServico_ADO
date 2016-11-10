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

        public ActionResult Cadastrar(OrdemServico os)
        {
            OrdemRepo.Cadastrar(os);
            return RedirectToAction("Index", "OrdemServico");
        }

        public ActionResult Editar(int id)
        {
            var os = OrdemRepo.PegarOrdem(id);
            return View(os);
        }

        public ActionResult SalvarEdicao(OrdemServico os)
        {
            OrdemRepo.Editar(os);
            return RedirectToAction("Index", "OrdemServico");
        }

        public ActionResult Excluir(int id)
        {
            OrdemRepo.Exclur(id);
            return RedirectToAction("Index", "OrdemServico");
        }
    }
}