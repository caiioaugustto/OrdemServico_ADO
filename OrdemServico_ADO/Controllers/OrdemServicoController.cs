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
            if(Uteis.SessionManager.IsAuthenticated)
            {
                IList<OrdemServico> listarOS = OrdemRepo.Listar();
                return View(listarOS);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult PreencherOrdemServico()
        {
            if(Uteis.SessionManager.IsAuthenticated)
            {
                ViewBag.Fornecedor = fornRepo.Listar();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Cadastrar(OrdemServico ordemServico)
        {
            if(Uteis.SessionManager.IsAuthenticated)
            {
                OrdemRepo.Cadastrar(ordemServico);

                return RedirectToAction("Index", "OrdemServico");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
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