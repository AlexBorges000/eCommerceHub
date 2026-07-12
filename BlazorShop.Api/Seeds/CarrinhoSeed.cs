using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds;

public class CarrinhoSeed
{
    public static readonly Carrinho[] Carrinho = CriarCarrinho();

    private static Carrinho[] CriarCarrinho()
    {
        return
        [
            new Carrinho
            {
            Id = 1,
                UsuarioId = 1

            },
            new Carrinho
            {
            Id = 2,
                UsuarioId = 2

            }
        ];
    }
}
