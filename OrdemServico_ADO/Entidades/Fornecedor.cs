using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string NomeResponsavel { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public string Descricao { get; set; }
    }
}