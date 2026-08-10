using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds;

public class UsuarioPjSeed()
{
    public static readonly UsuarioJuridico Usuario =

        new UsuarioJuridico
        {
            //senha = 123456
            Id = 2,
            Email = "compras@empresa.com.br",
            Telefone = "1133334444",
            Senha = "AQAAAAIAAYagAAAAEHBa642eVy878j89Ka4dX+wHVaHs2sxwPaA0pl0WaUNR/6GmiTitvk5lvoWzO7nFKA==",
            RoleId = 2,
            NomeFantasia = "Empresa XPTO",
            RazaoSocial = "Empresa XPTO Comércio LTDA",
            ResponsavelCompra = "João Silva",
            InscricaoEstadual = "123456789",
            HashCNPJ = "U8MZKklyxn4O80sLxVuZ9feCBjNQKlIdxRvy0D4G+7c=",
            EncryptCNPJ = "cK8XsbgyHhWuJYkw.fBGmxry9giCT9SsnTbEnpw==.EZO6vb7nH9hur/V8t/E=",
        };
}
