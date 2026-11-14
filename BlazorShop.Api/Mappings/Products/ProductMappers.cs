using BlazorShop.Models.DTOs.ProductDtos;
using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Mappings.Products;

public static class ProductMappers
{
    public static IEnumerable<ProductRequestDto> ToDto(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToDto());
    }

    public static ProductRequestDto ToDto(this Product product)
    {
        return new ProductRequestDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        };
    }
}
