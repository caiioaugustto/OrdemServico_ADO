using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class OrdemServico
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public string NumeroOrdemServico { get; set; }

        public int NumeroCondominio { get; set; }

        public string Solicitante { get; set; }

        public string Gerente { get; set; }

        public string Nucleo { get; set; }

        public DateTime DataEnvio { get; set; }

        public int Prazo { get; set; }

        public DateTime DataLiberacao { get; set; }

        public string Status { get; set; }

        public string DescricaoServico { get; set; }

        public int IdFornecedor { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public OrdemServico()
        {
            Fornecedor = new Fornecedor();
        }
    }
}