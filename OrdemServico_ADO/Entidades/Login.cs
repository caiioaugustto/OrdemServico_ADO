using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Login
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [MaxLength(30, ErrorMessage = "Máximo permitido para o Email são 30 caracteres.")]
        //[EmailAddress]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}