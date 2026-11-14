using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds
{
    public class IndividualUserSeed
    {
        public static readonly IndividualUser User =
           new IndividualUser
           {
               //senha = 123456
               Id= 1,
               Email = "alex@email.com",
               Phone = "11999999999",
               Password = "AQAAAAIAAYagAAAAEDRKDH1tUeR1uzSsIVa9zS3ljP3Figuiwxs5u9IJSOCxBcUgtQAhY8qHhKKRw/NY5w==",
               RoleId = 1,
               Name = "Alex Borges",
               EncryptTaxId = "NV6tGALKS92igfUJ.g6XgZW/bBbVi09kJP037dQ==.7461FoB/Z/fpLTg=",
               HashTaxId = "7vFUhvLFsZnsLTce6e8qqmdjZINTX40kHDIGEw0FzNo=",
           };
    }
}
