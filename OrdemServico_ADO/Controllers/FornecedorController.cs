﻿using Entidades;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            return View();
        }

        public ActionResult PreencherCadastro()
        {
            return View();
        }

        public ActionResult Cadastrar(Fornecedor fornecedor)
        {
            fornRepo.Cadastrar(fornecedor);
            //IList<Fornecedor> listarFornecedores = fornRepo.Listar();

            return RedirectToAction("Index", "Fornecedor");

            //return View("Index", listarFornecedores);
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

        public ActionResult Inativar(int id)
        {
            fornRepo.Inativar(id);
            return Content("ok");
        }

        public ActionResult Ativar(int id)
        {
            fornRepo.Ativar(id);
            return Content("ok");
        }

        public ActionResult Buscar(string nome, bool ativo)
        {
            var fornecedores = fornRepo.ListarFiltro(nome, ativo);

            return PartialView("partial/_Listar", fornecedores);

            //int paginaNumero = (pagina ?? 1);
            //int paginaTamanho = 20;

            //return PartialView("partial/_Listar", fornecedores.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Exportar(string nome, bool ativo)
        {
            IEnumerable<Fornecedor> fornecedores = fornRepo.ListarFiltro(nome, ativo);

            MemoryStream stream = new MemoryStream();

            using (ExcelPackage xlPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = xlPackage.Workbook.Worksheets.Add("Fornecedores");
                var numeroLinha = 2;

                ws.Cells[1, 1].Value = "Nome";
                ws.Cells[1, 2].Value = "Nome Responsavel";
                ws.Cells[1, 3].Value = "Número Fornecedor";
                ws.Cells[1, 4].Value = "Descrição";
                ws.Cells[1, 5].Value = "Email";
                ws.Cells[1, 6].Value = "Telefone";

                ws.Cells[1, 1, 1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, 6].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0000CD"));
                ws.Cells[1, 1, 1, 6].Style.Font.Bold = true;
                ws.Cells[1, 1, 1, 6].Style.Font.Size = 14;
                ws.Cells[1, 1, 1, 6].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                foreach (var fornecedor in fornecedores)
                {
                    ws.Cells[numeroLinha, 1].Value = fornecedor.Nome;
                    ws.Cells[numeroLinha, 2].Value = fornecedor.NomeResponsavel;
                    ws.Cells[numeroLinha, 3].Value = fornecedor.NumeroFornecedor;
                    ws.Cells[numeroLinha, 4].Value = fornecedor.Descricao;
                    ws.Cells[numeroLinha, 5].Value = fornecedor.Email;
                    ws.Cells[numeroLinha, 6].Value = fornecedor.Telefone;

                    numeroLinha++;
                }

                ws.Cells[1, 1, numeroLinha, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1, numeroLinha, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1, numeroLinha, 6].Style.WrapText = true;

                ws.Column(1).Width = 40;
                ws.Column(2).Width = 40;
                ws.Column(3).Width = 40;
                ws.Column(4).Width = 30;
                ws.Column(5).Width = 50;
                ws.Column(6).Width = 20;

                xlPackage.Workbook.Properties.Company = "Exportação Excel";

                stream = new MemoryStream(xlPackage.GetAsByteArray());
            }
            return File(stream, "application/xls", "Fornecedores.xlsx");
        }
    }
}