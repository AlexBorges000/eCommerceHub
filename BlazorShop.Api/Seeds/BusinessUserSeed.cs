using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds;

public class BusinessUserSeed()
{
    public static readonly BusinessUser User =

        new BusinessUser
        {
            //senha = 123456
            Id = 2,
            Email = "compras@empresa.com.br",
            //Endereco = "Av. Paulista, 1000",
            Phone = "1133334444",
            Password = "AQAAAAIAAYagAAAAEHBa642eVy878j89Ka4dX+wHVaHs2sxwPaA0pl0WaUNR/6GmiTitvk5lvoWzO7nFKA==",
            RoleId = 2,
            TradeName = "Empresa XPTO",
            LegalName = "Empresa XPTO Comércio LTDA",
            PurchasingContact = "João Silva",
            StateRegistration = "123456789",
            HashCnpj = "U8MZKklyxn4O80sLxVuZ9feCBjNQKlIdxRvy0D4G+7c=",
            EncryptCnpj = "cK8XsbgyHhWuJYkw.fBGmxry9giCT9SsnTbEnpw==.EZO6vb7nH9hur/V8t/E=",
        };
}
