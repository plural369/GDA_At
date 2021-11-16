using Gerenciador.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
     
        [Required]
        public string Nome { get; set; }
       [Required]
        public string Cpf { get; set; }
       [Required]
        public string Rg { get; set; }
       [Required]
        public string Matricula { get; set; }
       [Required]
        public int? CursoId { get; set; }

        public Curso Curso { get; set; }

    }
}
