using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
        
    }
}
