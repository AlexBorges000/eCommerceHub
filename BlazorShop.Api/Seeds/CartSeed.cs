using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds;

public class CartSeed
{
    public static readonly Cart[] Cart = CreateCart();

    private static Cart[] CreateCart()
    {
        return
        [
            new Cart
            {
            Id = 1,
                UserId = 1

            },
            new Cart
            {
            Id = 2,
                UserId = 2

            }
        ];
    }
}
