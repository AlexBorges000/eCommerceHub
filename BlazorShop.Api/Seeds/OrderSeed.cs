using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Seeds;

public class OrderSeed
{
    public static readonly Order[] Order = CreateOrder();

    private static Order[] CreateOrder()
    {
        return
        [
            new Order
            {
            Id = 1,
                UserId = 1

            },
            new Order
            {
            Id = 2,
                UserId = 2

            }
        ];
    }
}
