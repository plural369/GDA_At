using GERENCIADOR_TESTE_TEMPLANTE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<FileModel> fileModel { get; set; }
    }
}
