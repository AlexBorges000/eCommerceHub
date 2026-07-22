using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds
{
    public class EnderecoSeed
    {
        public static Endereco[] Endereco = CriarEndereco();
        private static Endereco[] CriarEndereco()
        {
            return
            [
                new Endereco
                {
                    Id = 1,
                    UsuarioId = 1,
                    Logradouro = "Avenida Paulista",
                    Numero = "1578",
                    Complemento = "Apartamento 82, Torre B",
                    Bairro = "Bela Vista",
                    Cidade = "São Paulo",
                    UF = "SP",
                    CEP = "01310-200",
                    Principal = true
                },
                new Endereco
                {
                    Id = 2,
                    UsuarioId = 2,
                    Logradouro = "Rua Doutor Antônio Carlos de Souza",
                    Numero = "245A",
                    Complemento = "Casa dos fundos, entrada lateral",
                    Bairro = "Jardim Primavera",
                    Cidade = "Campinas",
                    UF = "SP",
                    CEP = "13010-050",
                    Principal = true
                }
            ];
        }
    }
}
