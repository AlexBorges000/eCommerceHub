using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.ProductDtos;

namespace BlazorShop.Api.Mappings.Categories;

public static class CategoryMappers
{
    public static IEnumerable<ProductCategoryResponseDto> ToDto(this IEnumerable<Category> categories)
    {
        return (from category in categories
                select new ProductCategoryResponseDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    IconCSS = category.IconCSS
                }).ToList();
    }
}
