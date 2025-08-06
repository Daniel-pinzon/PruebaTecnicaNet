using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Camposuariocodigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Codigos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codigos_UsuarioId",
                table: "Codigos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Codigos_Usuarios_UsuarioId",
                table: "Codigos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codigos_Usuarios_UsuarioId",
                table: "Codigos");

            migrationBuilder.DropIndex(
                name: "IX_Codigos_UsuarioId",
                table: "Codigos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Codigos");
        }
    }
}
