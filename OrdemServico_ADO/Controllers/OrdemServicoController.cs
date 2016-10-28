using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controllers
{
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

        public void Cadastrar(OrdemServico ordemServico)
        {
            OrdemRepo.Cadastrar(ordemServico);

            Index();
        }
    }
}