using Entidades;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OrdemServico_ADO.Services;
using Repository;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using Uteis;

namespace Controllers
{
    [AutorizacaoFilter]
    public class OrdemServicoController : Controller
    {
        //OrdemServicoRepository ordemRepo = new OrdemServicoRepository();
        private OrdemServicoRepository ordemRepo;

        FornecedorRepository fornRepo = new FornecedorRepository();

        public OrdemServicoController(OrdemServicoRepository ordemRepo)
        {
            this.ordemRepo = ordemRepo;
        }

        public ActionResult Index()
        {
            IList<OrdemServico> listarOS = ordemRepo.Listar();
            return View(listarOS);
        }

        public ActionResult PreencherOrdemServico()
        {
            ViewBag.NumeroOS = GerarOrdemServico.GerarOS();
            ViewBag.Fornecedor = fornRepo.ListarNomeId();
            
            return View();
        }

        public ActionResult Detalhes(int id)
        {
            var detalhesOS = ordemRepo.Detalhes(id);

            return View(detalhesOS);
        }

        public ActionResult Cadastrar(OrdemServico os)
        {
            if(ModelState.IsValid)
            {
                ordemRepo.Cadastrar(os);
                return RedirectToAction("Index", "OrdemServico");
            }
            else
            {
                return View(os);
            }
        }

        public ActionResult Editar(int id)
        {
            var os = ordemRepo.PegarOrdem(id);
            return View(os);
        }

        public ActionResult SalvarEdicao(OrdemServico os)
        {
            ordemRepo.Editar(os);
            return RedirectToAction("Index", "OrdemServico");
        }

        public ActionResult Buscar(string nome, bool ativo)
        {
            var ordensServico = ordemRepo.ListarFiltro(nome, ativo);

            return PartialView("partial/_Listar", ordensServico);

            //int paginaNumero = (pagina ?? 1);
            //int paginaTamanho = 20;

            //return PartialView("partial/_Listar", fornecedores.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Exportar(string nome)
        {
            IEnumerable<OrdemServico> ordensServicos = ordemRepo.Listar();

            MemoryStream stream = new MemoryStream();

            using (ExcelPackage xlPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = xlPackage.Workbook.Worksheets.Add("Ordem de Serviço");
                var numeroLinha = 2;

                ws.Cells[1, 1].Value = "Nome Fornecedor";
                ws.Cells[1, 2].Value = "Nº OS";
                ws.Cells[1, 3].Value = "Data de Solicitação";
                ws.Cells[1, 4].Value = "Prazo";
                ws.Cells[1, 5].Value = "Solicitante";
                ws.Cells[1, 6].Value = "Data de Envio";
                ws.Cells[1, 7].Value = "Núcleo";
                ws.Cells[1, 8].Value = "Gerente";
                ws.Cells[1, 9].Value = "Descrição OS";
                ws.Cells[1, 10].Value = "Status";

                ws.Cells[1, 1, 1, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, 10].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0000CD"));
                ws.Cells[1, 1, 1, 10].Style.Font.Bold = true;
                ws.Cells[1, 1, 1, 10].Style.Font.Size = 14;
                ws.Cells[1, 1, 1, 10].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
               
                foreach (var os in ordensServicos)
                {
                    ws.Cells[numeroLinha, 1].Value = os.Fornecedor.Nome;
                    ws.Cells[numeroLinha, 2].Value = os.NumeroOrdemServico;
                    ws.Cells[numeroLinha, 3].Value = os.DataSolicitacao;
                    ws.Cells[numeroLinha, 4].Value = os.Prazo;
                    ws.Cells[numeroLinha, 5].Value = os.Solicitante;
                    ws.Cells[numeroLinha, 6].Value = os.DataEnvio;
                    ws.Cells[numeroLinha, 7].Value = os.Nucleo;
                    ws.Cells[numeroLinha, 8].Value = os.Gerente;
                    ws.Cells[numeroLinha, 9].Value = os.DescricaoServico;
                    ws.Cells[numeroLinha, 10].Value = os.Status;

                    numeroLinha++;
                }

                ws.Cells[1, 1, numeroLinha, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1, numeroLinha, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1, numeroLinha, 10].Style.WrapText = true;

                ws.Column(1).Width = 40;
                ws.Column(2).Width = 40;
                ws.Column(3).Width = 40;
                ws.Column(4).Width = 30;
                ws.Column(5).Width = 50;
                ws.Column(6).Width = 20;
                ws.Column(7).Width = 20;
                ws.Column(8).Width = 20;
                ws.Column(9).Width = 50;
                ws.Column(10).Width = 20;
                
                xlPackage.Workbook.Properties.Company = "Exportação Excel";

                stream = new MemoryStream(xlPackage.GetAsByteArray());
            }

            return File(stream, "application/xls", "OrdemServico.xlsx");
        }

        public ActionResult Inativar(int id)
        {
            ordemRepo.Inativar(id);
            return RedirectToAction("Index", "OrdemServico");
        }
    }
}