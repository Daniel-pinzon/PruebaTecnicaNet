using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Camposuariocodigoajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codigos_Usuarios_UsuarioId",
                table: "Codigos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Codigos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Codigos_Usuarios_UsuarioId",
                table: "Codigos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codigos_Usuarios_UsuarioId",
                table: "Codigos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Codigos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Codigos_Usuarios_UsuarioId",
                table: "Codigos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
