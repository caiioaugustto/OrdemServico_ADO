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

        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        [StringLength(20, MinimumLength = 3)]
        public string NumeroOrdemServico { get; set; }

        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        [StringLength(30, MinimumLength = 3)]
        public string Solicitante { get; set; }

        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        [StringLength(30, MinimumLength = 3)]
        public string Gerente { get; set; }

        public string Nucleo { get; set; }

        public DateTime DataEnvio { get; set; }

        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        public DateTime Prazo { get; set; }

        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        public DateTime DataLiberacao { get; set; }

        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        //public string Status { get; set; }
        public Status Status { get; set; }

        [Required(ErrorMessage = "O campo ID do Veiculo é obrigatório")]
        public Descricao DescricaoServico { get; set; }

        public int IdFornecedor { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public OrdemServico()
        {
            Fornecedor = new Fornecedor();
        }
    }
}