using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds
{
    public class AddressSeed
    {
        public static Address[] Address = CreateAddress();
        private static Address[] CreateAddress()
        {
            return
            [
                new Address
                {
                    Id = 1,
                    UserId = 1,
                    Street = "Avenida Paulista",
                    Number = "1578",
                    Complement = "Apartamento 82, Torre B",
                    District = "Bela Vista",
                    City = "São Paulo",
                    UF = "SP",
                    PostalCode = "01310-200",
                    IsPrimary = true
                },
                new Address
                {
                    Id = 2,
                    UserId = 2,
                    Street = "Rua Doutor Antônio Carlos de Souza",
                    Number = "245A",
                    Complement = "Casa dos fundos, entrada lateral",
                    District = "Jardim Primavera",
                    City = "Campinas",
                    UF = "SP",
                    PostalCode = "13010-050",
                    IsPrimary = true
                }
            ];
        }
    }
}
