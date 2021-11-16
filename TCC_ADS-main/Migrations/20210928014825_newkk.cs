using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador.Migrations
{
    public partial class newkk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tipoPasta",
                table: "aluno",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipoPasta",
                table: "aluno");
        }
    }
}
