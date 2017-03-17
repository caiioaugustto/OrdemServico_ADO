using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nº Fornecedor")]
        public int NumeroFornecedor { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Responsável")]
        public string NomeResponsavel { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }
    }
}