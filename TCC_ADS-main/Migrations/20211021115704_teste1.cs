using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador.Migrations
{
    public partial class teste1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilesOnDatabase",
                table: "FilesOnDatabase");

            migrationBuilder.DropColumn(
                name: "Disciplina",
                table: "professor");

            migrationBuilder.DropColumn(
                name: "RF",
                table: "professor");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ra",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FilesOnDatabase");

            migrationBuilder.RenameTable(
                name: "FilesOnDatabase",
                newName: "FileModel");

            migrationBuilder.RenameColumn(
                name: "idUsuario",
                table: "FileModel",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "FileModel",
                newName: "TipoArquivo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileModel",
                newName: "NomeArquivo");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "FileModel",
                newName: "Estensao");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "FileModel",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "FileModel",
                newName: "DataCriacao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "professor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rg",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AnoPubli",
                table: "FileModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "FileModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "FileModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "FileModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoTrabalhoId",
                table: "FileModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "FileModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipoTrabalho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoTrabalho", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CursoId",
                table: "AspNetUsers",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_applicationUserId",
                table: "FileModel",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_CategoriaId",
                table: "FileModel",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_ProfessorId",
                table: "FileModel",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_TipoTrabalhoId",
                table: "FileModel",
                column: "TipoTrabalhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_curso_CursoId",
                table: "AspNetUsers",
                column: "CursoId",
                principalTable: "curso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_AspNetUsers_applicationUserId",
                table: "FileModel",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_categoria_CategoriaId",
                table: "FileModel",
                column: "CategoriaId",
                principalTable: "categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_professor_ProfessorId",
                table: "FileModel",
                column: "ProfessorId",
                principalTable: "professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_tipoTrabalho_TipoTrabalhoId",
                table: "FileModel",
                column: "TipoTrabalhoId",
                principalTable: "tipoTrabalho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_curso_CursoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_AspNetUsers_applicationUserId",
                table: "FileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_categoria_CategoriaId",
                table: "FileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_professor_ProfessorId",
                table: "FileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_tipoTrabalho_TipoTrabalhoId",
                table: "FileModel");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "tipoTrabalho");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CursoId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_applicationUserId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_CategoriaId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_ProfessorId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_TipoTrabalhoId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rg",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AnoPubli",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "TipoTrabalhoId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "FileModel");

            migrationBuilder.RenameTable(
                name: "FileModel",
                newName: "FilesOnDatabase");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "FilesOnDatabase",
                newName: "idUsuario");

            migrationBuilder.RenameColumn(
                name: "TipoArquivo",
                table: "FilesOnDatabase",
                newName: "UploadedBy");

            migrationBuilder.RenameColumn(
                name: "NomeArquivo",
                table: "FilesOnDatabase",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Estensao",
                table: "FilesOnDatabase",
                newName: "FileType");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "FilesOnDatabase",
                newName: "Extension");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "FilesOnDatabase",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "professor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disciplina",
                table: "professor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RF",
                table: "professor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ra",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FilesOnDatabase",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilesOnDatabase",
                table: "FilesOnDatabase",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disciplina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Integrantes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoPasta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.Id);
                });
        }
    }
}
