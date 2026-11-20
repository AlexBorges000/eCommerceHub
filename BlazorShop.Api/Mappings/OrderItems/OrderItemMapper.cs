using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.OrderDtos;

namespace BlazorShop.Api.Mappings.OrderItems;

public static class OrderItemMapper
{
    public static IEnumerable<OrderItemDto> ToDto(this IEnumerable<OrderItem> orderItems)
    {
        return orderItems.Select(item => item.ToDto());
    }

    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            Id = orderItem.Id,
            ProductId = orderItem.ProductId,
            ProductName = orderItem.Product.Name,
            ProductDescription = orderItem.Product.Description,
            ProductImageURL = orderItem.Product.ImageUrl,
            Price = orderItem.Product.Price,
            OrderId = orderItem.OrderId,
            Quantity = orderItem.Quantity,
            PriceTotal = orderItem.Product.Price * orderItem.Quantity
        };

    }
}
