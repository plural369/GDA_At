using GERENCIADOR_TESTE_TEMPLANTE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Models
{
    public class CateTipoFile
    {
        public ICollection<FileOnDatabaseModel> FileOnDatabaseModel { get; set; }
        public FileOnDatabaseModel FileOnDatabaseModelok { get; set; }
        public ICollection<Categoria> categoria { get; set; }
        public Categoria categoriaa { get; set; }
        public ICollection<TipoTrabalho> tipoTrabalho { get; set; }
        public TipoTrabalho tipoTrabalhoo { get; set; }
        public ICollection<Professor> professor { get; set; }
        public Professor professorr { get; set; }
    }
}
