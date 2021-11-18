using Gerenciador.Models;
using GERENCIADOR_TESTE_TEMPLANTE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Context
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> options): base(options)
        { }
        public DbSet<Curso> curso { get; set; }
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<TipoTrabalho> tipoTrabalho { get; set; }
        public DbSet<Professor> professor { get; set; }
        public DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    }
}
