using Microsoft.EntityFrameworkCore.Migrations;

namespace capitulo01.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InstituicaoId",
                table: "Departamentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_InstituicaoId",
                table: "Departamentos",
                column: "InstituicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoId",
                table: "Departamentos",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoId",
                table: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_InstituicaoId",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Departamentos");
        }
    }
}
