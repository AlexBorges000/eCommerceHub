using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class TPT_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinho_Usuarios_UsuarioId",
                table: "Carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                table: "CarrinhoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_Produtos_ProdutoId",
                table: "CarrinhoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Usuarios_UsuarioId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Role_RoleId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Id",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_CNPJ",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_HashCpf",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrinhoItem",
                table: "CarrinhoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EncryptCpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "HashCpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ResponsavelCompra",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "CarrinhoItem",
                newName: "CarrinhoItens");

            migrationBuilder.RenameTable(
                name: "Carrinho",
                newName: "Carrinhos");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_RoleId",
                table: "Usuario",
                newName: "IX_Usuario_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "Usuario",
                newName: "IX_Usuario_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Name",
                table: "Roles",
                newName: "IX_Roles_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoItem_ProdutoId",
                table: "CarrinhoItens",
                newName: "IX_CarrinhoItens_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoItem_CarrinhoId",
                table: "CarrinhoItens",
                newName: "IX_CarrinhoItens_CarrinhoId");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinho_UsuarioId",
                table: "Carrinhos",
                newName: "IX_Carrinhos_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrinhoItens",
                table: "CarrinhoItens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinhos",
                table: "Carrinhos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsuarioFisico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EncryptCpf = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HashCpf = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioFisico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioFisico_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioJuridico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResponsavelCompra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HashCNPJ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EncryptCNPJ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioJuridico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioJuridico_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "ImagemUrl", "Nome", "Preco", "Quantidade" },
                values: new object[] { 54, 1, "Teste", "Teste", "Teste", 100m, 100 });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "AQAAAAIAAYagAAAAEDRKDH1tUeR1uzSsIVa9zS3ljP3Figuiwxs5u9IJSOCxBcUgtQAhY8qHhKKRw/NY5w==");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 2,
                column: "Senha",
                value: "AQAAAAIAAYagAAAAEHBa642eVy878j89Ka4dX+wHVaHs2sxwPaA0pl0WaUNR/6GmiTitvk5lvoWzO7nFKA==");

            migrationBuilder.InsertData(
                table: "UsuarioFisico",
                columns: new[] { "Id", "EncryptCpf", "HashCpf", "Nome" },
                values: new object[] { 1, "NV6tGALKS92igfUJ.g6XgZW/bBbVi09kJP037dQ==.7461FoB/Z/fpLTg=", "7vFUhvLFsZnsLTce6e8qqmdjZINTX40kHDIGEw0FzNo=", "Alex Borges" });

            migrationBuilder.InsertData(
                table: "UsuarioJuridico",
                columns: new[] { "Id", "EncryptCNPJ", "HashCNPJ", "InscricaoEstadual", "NomeFantasia", "RazaoSocial", "ResponsavelCompra" },
                values: new object[] { 2, "cK8XsbgyHhWuJYkw.fBGmxry9giCT9SsnTbEnpw==.EZO6vb7nH9hur/V8t/E=", "U8MZKklyxn4O80sLxVuZ9feCBjNQKlIdxRvy0D4G+7c=", "123456789", "Empresa XPTO", "Empresa XPTO Comércio LTDA", "João Silva" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome",
                table: "Produtos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioFisico_HashCpf",
                table: "UsuarioFisico",
                column: "HashCpf",
                unique: true,
                filter: "[HashCpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJuridico_HashCNPJ",
                table: "UsuarioJuridico",
                column: "HashCNPJ",
                unique: true,
                filter: "[HashCNPJ] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItens_Carrinhos_CarrinhoId",
                table: "CarrinhoItens",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItens_Produtos_ProdutoId",
                table: "CarrinhoItens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_Usuario_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Usuario_UsuarioId",
                table: "RefreshTokens",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Roles_RoleId",
                table: "Usuario",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItens_Carrinhos_CarrinhoId",
                table: "CarrinhoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItens_Produtos_ProdutoId",
                table: "CarrinhoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_Usuario_UsuarioId",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Usuario_UsuarioId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Roles_RoleId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "UsuarioFisico");

            migrationBuilder.DropTable(
                name: "UsuarioJuridico");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_Nome",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinhos",
                table: "Carrinhos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrinhoItens",
                table: "CarrinhoItens");

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Carrinhos",
                newName: "Carrinho");

            migrationBuilder.RenameTable(
                name: "CarrinhoItens",
                newName: "CarrinhoItem");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_RoleId",
                table: "Usuarios",
                newName: "IX_Usuarios_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_Email",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Name",
                table: "Role",
                newName: "IX_Role_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinho",
                newName: "IX_Carrinho_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoItens_ProdutoId",
                table: "CarrinhoItem",
                newName: "IX_CarrinhoItem_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoItens_CarrinhoId",
                table: "CarrinhoItem",
                newName: "IX_CarrinhoItem_CarrinhoId");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Usuarios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncryptCpf",
                table: "Usuarios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashCpf",
                table: "Usuarios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelCompra",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPessoa",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrinhoItem",
                table: "CarrinhoItem",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CNPJ", "EncryptCpf", "HashCpf", "InscricaoEstadual", "Nome", "NomeFantasia", "RazaoSocial", "ResponsavelCompra", "Senha", "TipoPessoa" },
                values: new object[] { null, "12345678901", null, null, "Alex Borges", null, null, null, "123456", 0 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CNPJ", "EncryptCpf", "HashCpf", "InscricaoEstadual", "Nome", "NomeFantasia", "RazaoSocial", "ResponsavelCompra", "Senha", "TipoPessoa" },
                values: new object[] { "12345678000199", null, null, "123456789", null, "Empresa XPTO", "Empresa XPTO Comércio LTDA", "João Silva", "123456", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Id",
                table: "RefreshTokens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CNPJ",
                table: "Usuarios",
                column: "CNPJ",
                unique: true,
                filter: "[CNPJ] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_HashCpf",
                table: "Usuarios",
                column: "HashCpf",
                unique: true,
                filter: "[HashCpf] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinho_Usuarios_UsuarioId",
                table: "Carrinho",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                table: "CarrinhoItem",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_Produtos_ProdutoId",
                table: "CarrinhoItem",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Usuarios_UsuarioId",
                table: "RefreshTokens",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Role_RoleId",
                table: "Usuarios",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
