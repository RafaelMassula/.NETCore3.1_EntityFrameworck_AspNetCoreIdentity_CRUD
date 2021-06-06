using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Migrations
{
    public partial class updateTableInstituicoesFromInstituicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituicaos",
                table: "Instituicaos");

            migrationBuilder.RenameTable(
                name: "Instituicaos",
                newName: "Instituicoes");

            migrationBuilder.RenameColumn(
                name: "InstituicaoID",
                table: "Instituicoes",
                newName: "InstituicaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituicoes",
                table: "Instituicoes",
                column: "InstituicaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituicoes",
                table: "Instituicoes");

            migrationBuilder.RenameTable(
                name: "Instituicoes",
                newName: "Instituicaos");

            migrationBuilder.RenameColumn(
                name: "InstituicaoId",
                table: "Instituicaos",
                newName: "InstituicaoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituicaos",
                table: "Instituicaos",
                column: "InstituicaoID");
        }
    }
}
