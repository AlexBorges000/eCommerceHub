using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorShop.Api.Migrations;

/// <inheritdoc />
public partial class _20261114125705_EnglishEntityNames : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_RefreshTokens_Usuario_UsuarioId",
            table: "RefreshTokens");

        migrationBuilder.RenameColumn(
            name: "UsuarioId",
            table: "RefreshTokens",
            newName: "UserId");

        migrationBuilder.RenameIndex(
            name: "IX_RefreshTokens_UsuarioId",
            table: "RefreshTokens",
            newName: "IX_RefreshTokens_UserId");

        migrationBuilder.AddColumn<string>(
            name: "Slug",
            table: "Produtos",
            type: "nvarchar(250)",
            maxLength: 250,
            nullable: false,
            defaultValue: "");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Beleza/Beleza1.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Beleza/Beleza2.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Beleza/Beleza3.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Beleza/Beleza4.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 5,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Beleza/Beleza5.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 6,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Eletronicos/eletronico1.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 7,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Eletronicos/eletronico2.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 8,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Eletronicos/eletronico3.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 9,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Eletronicos/eletronico4.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 10,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Eletronicos/eletronico5.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 11,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Eletronicos/tecnologia6.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 12,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Moveis/moveis1.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 13,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Moveis/moveis2.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 14,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Moveis/moveis3.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 15,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Moveis/moveis4.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 16,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Moveis/moveis6.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 17,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Moveis/moveis7.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 18,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Calcados/calcado1.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 19,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Calcados/calcado2.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 20,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Calcados/calcado3.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 21,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Calcados/calcado4.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 22,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Calcados/calcado5.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 23,
            columns: new[] { "ImagemUrl", "Slug" },
            values: new object[] { "/Images/Calcados/calcado6.png", "" });

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 54,
            column: "Slug",
            value: "");

        migrationBuilder.AddForeignKey(
            name: "FK_RefreshTokens_Usuario_UserId",
            table: "RefreshTokens",
            column: "UserId",
            principalTable: "Usuario",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_RefreshTokens_Usuario_UserId",
            table: "RefreshTokens");

        migrationBuilder.DropColumn(
            name: "Slug",
            table: "Produtos");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "RefreshTokens",
            newName: "UsuarioId");

        migrationBuilder.RenameIndex(
            name: "IX_RefreshTokens_UserId",
            table: "RefreshTokens",
            newName: "IX_RefreshTokens_UsuarioId");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 1,
            column: "ImagemUrl",
            value: "/Imagens/Beleza/Beleza1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 2,
            column: "ImagemUrl",
            value: "/Imagens/Beleza/Beleza2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 3,
            column: "ImagemUrl",
            value: "/Imagens/Beleza/Beleza3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 4,
            column: "ImagemUrl",
            value: "/Imagens/Beleza/Beleza4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 5,
            column: "ImagemUrl",
            value: "/Imagens/Beleza/Beleza5.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 6,
            column: "ImagemUrl",
            value: "/Imagens/Eletronicos/eletronico1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 7,
            column: "ImagemUrl",
            value: "/Imagens/Eletronicos/eletronico2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 8,
            column: "ImagemUrl",
            value: "/Imagens/Eletronicos/eletronico3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 9,
            column: "ImagemUrl",
            value: "/Imagens/Eletronicos/eletronico4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 10,
            column: "ImagemUrl",
            value: "/Imagens/Eletronicos/eletronico5.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 11,
            column: "ImagemUrl",
            value: "/Imagens/Eletronicos/tecnologia6.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 12,
            column: "ImagemUrl",
            value: "/Imagens/Moveis/moveis1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 13,
            column: "ImagemUrl",
            value: "/Imagens/Moveis/moveis2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 14,
            column: "ImagemUrl",
            value: "/Imagens/Moveis/moveis3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 15,
            column: "ImagemUrl",
            value: "/Imagens/Moveis/moveis4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 16,
            column: "ImagemUrl",
            value: "/Imagens/Moveis/moveis6.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 17,
            column: "ImagemUrl",
            value: "/Imagens/Moveis/moveis7.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 18,
            column: "ImagemUrl",
            value: "/Imagens/Calcados/calcado1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 19,
            column: "ImagemUrl",
            value: "/Imagens/Calcados/calcado2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 20,
            column: "ImagemUrl",
            value: "/Imagens/Calcados/calcado3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 21,
            column: "ImagemUrl",
            value: "/Imagens/Calcados/calcado4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 22,
            column: "ImagemUrl",
            value: "/Imagens/Calcados/calcado5.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 23,
            column: "ImagemUrl",
            value: "/Imagens/Calcados/calcado6.png");

        migrationBuilder.AddForeignKey(
            name: "FK_RefreshTokens_Usuario_UsuarioId",
            table: "RefreshTokens",
            column: "UsuarioId",
            principalTable: "Usuario",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
