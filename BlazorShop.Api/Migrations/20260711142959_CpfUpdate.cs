using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class CpfUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_CPF",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Usuarios",
                newName: "HashCpf");

            migrationBuilder.AddColumn<string>(
                name: "EncryptCpf",
                table: "Usuarios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EncryptCpf", "HashCpf" },
                values: new object[] { "12345678901", null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "EncryptCpf",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_HashCpf",
                table: "Usuarios",
                column: "HashCpf",
                unique: true,
                filter: "[HashCpf] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_HashCpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EncryptCpf",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "HashCpf",
                table: "Usuarios",
                newName: "CPF");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CPF",
                value: "12345678901");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CPF",
                table: "Usuarios",
                column: "CPF",
                unique: true,
                filter: "[CPF] IS NOT NULL");
        }
    }
}
