using Gerenciador.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GERENCIADOR_TESTE_TEMPLANTE.Models
{
    public class FileModel
    {
        //Dados Base do arquivo
        public  int Id { get; set; } 
        [Required]
        public string Autor { get; set; }
        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        public string Cidade { get; set; }
        [Required]
        [Display(Name = "Palavra Chave")]
        public string PalavraChave { get; set; }
        [Display(Name = "Nome do Arquivo")]
        public string NomeArquivo { get; set; }
        [Display(Name = "Tipo do arquivo")]
        public string TipoArquivo { get; set; }
        [Display(Name = "Extensão")]
        public string Estensao { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Required]
        [Display(Name = "Ano de Publicação")]
        public int AnoPubli { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Display(Name = "Data de Criação")]
        public DateTime? DataCriacao { get; set; }

        //Relacionado
        public string UsuarioId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        //Relacionado
        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; }
        
        //Relacionado
        public int TipoTrabalhoId { get; set; }
        public TipoTrabalho TipoTrabalho { get; set; }


        //Relacionado
        public int ProfessorId { get; set; }
        public Professor professor { get; set; }

       
    }
}