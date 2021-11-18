using GERENCIADOR_TESTE_TEMPLANTE.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Matricula { get; set; }
        public int? CursoId { get; set; }
        public Curso Curso { get; set; }

        public ICollection<FileModel> fileModel { get; set; }


    }
}
