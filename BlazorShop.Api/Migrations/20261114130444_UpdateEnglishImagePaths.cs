using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorShop.Api.Migrations;

/// <inheritdoc />
public partial class _20261114130444_UpdateEnglishImagePaths : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 1,
            column: "ImagemUrl",
            value: "/images/Beauty/Beauty1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 2,
            column: "ImagemUrl",
            value: "/images/Beauty/Beauty2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 3,
            column: "ImagemUrl",
            value: "/images/Beauty/Beauty3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 4,
            column: "ImagemUrl",
            value: "/images/Beauty/Beauty4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 5,
            column: "ImagemUrl",
            value: "/images/Beauty/Beauty5.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 6,
            column: "ImagemUrl",
            value: "/images/Electronics/electronics1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 7,
            column: "ImagemUrl",
            value: "/images/Electronics/electronics2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 8,
            column: "ImagemUrl",
            value: "/images/Electronics/electronics3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 9,
            column: "ImagemUrl",
            value: "/images/Electronics/electronics4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 10,
            column: "ImagemUrl",
            value: "/images/Electronics/electronics5.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 11,
            column: "ImagemUrl",
            value: "/images/Electronics/technology6.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 12,
            column: "ImagemUrl",
            value: "/images/Furniture/furniture1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 13,
            column: "ImagemUrl",
            value: "/images/Furniture/furniture2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 14,
            column: "ImagemUrl",
            value: "/images/Furniture/furniture3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 15,
            column: "ImagemUrl",
            value: "/images/Furniture/furniture4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 16,
            column: "ImagemUrl",
            value: "/images/Furniture/furniture6.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 17,
            column: "ImagemUrl",
            value: "/images/Furniture/furniture7.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 18,
            column: "ImagemUrl",
            value: "/images/Shoes/shoe1.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 19,
            column: "ImagemUrl",
            value: "/images/Shoes/shoe2.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 20,
            column: "ImagemUrl",
            value: "/images/Shoes/shoe3.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 21,
            column: "ImagemUrl",
            value: "/images/Shoes/shoe4.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 22,
            column: "ImagemUrl",
            value: "/images/Shoes/shoe5.png");

        migrationBuilder.UpdateData(
            table: "Produtos",
            keyColumn: "Id",
            keyValue: 23,
            column: "ImagemUrl",
            value: "/images/Shoes/shoe6.png");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
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
    }
}
