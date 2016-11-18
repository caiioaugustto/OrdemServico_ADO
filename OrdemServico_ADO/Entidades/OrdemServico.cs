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

        [Display(Name = "Data da Solicitação")]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name = "Nº OS")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        [StringLength(20, MinimumLength = 3)]
        public string NumeroOrdemServico { get; set; }

        [Display(Name = "Solicitante")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        [StringLength(30, MinimumLength = 3)]
        public string Solicitante { get; set; }

        [Display(Name = "Gerente")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        [StringLength(30, MinimumLength = 3)]
        public string Gerente { get; set; }

        [Display(Name = "Núcleo")]
        public string Nucleo { get; set; }

        [Display(Name = "Data de Envio")]
        public DateTime DataEnvio { get; set; }

        [Display(Name = "Prazo")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        public DateTime Prazo { get; set; }

        [Display(Name = "Data de Liberação")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        public DateTime DataLiberacao { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        //public string Status { get; set; }
        public Status Status { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo abaixo é um campo Obrigatório")]
        public Descricao DescricaoServico { get; set; }

        public int IdFornecedor { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public OrdemServico()
        {
            Fornecedor = new Fornecedor();
        }
    }
}