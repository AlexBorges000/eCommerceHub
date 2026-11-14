using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.CartDtos;

namespace BlazorShop.Api.Mappings.CartItems;

public static class CartItemMapper
{
    public static IEnumerable<CartItemDto> ToDto(this IEnumerable<CartItem> cartItems)
    {
        return cartItems.Select(item => item.ToDto());
    }

    public static CartItemDto ToDto(this CartItem cartItem)
    {
        return new CartItemDto
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            ProductName = cartItem.Product.Name,
            ProductDescription = cartItem.Product.Description,
            ProductImageURL = cartItem.Product.ImageUrl,
            Price = cartItem.Product.Price,
            CartId = cartItem.CartId,
            Quantity = cartItem.Quantity,
            PriceTotal = cartItem.Product.Price * cartItem.Quantity
        };

    }
}
