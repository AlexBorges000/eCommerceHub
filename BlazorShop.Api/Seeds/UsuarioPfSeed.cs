using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds
{
    public class UsuarioPfSeed
    {
        public static readonly UsuarioFisico Usuario =
           new UsuarioFisico
           {
               //senha = 123456
               Id= 1,
               Email = "alex@email.com",
               Telefone = "11999999999",
               Senha = "AQAAAAIAAYagAAAAEDRKDH1tUeR1uzSsIVa9zS3ljP3Figuiwxs5u9IJSOCxBcUgtQAhY8qHhKKRw/NY5w==",
               RoleId = 1,
               Nome = "Alex Borges",
               EncryptCpf = "NV6tGALKS92igfUJ.g6XgZW/bBbVi09kJP037dQ==.7461FoB/Z/fpLTg=",
               HashCpf = "7vFUhvLFsZnsLTce6e8qqmdjZINTX40kHDIGEw0FzNo=",
           };
    }
}
