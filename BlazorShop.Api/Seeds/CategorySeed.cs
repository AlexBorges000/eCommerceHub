using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlazorShop.Api.Seeds;

public class CategorySeed
{
    public static readonly Category[] Category = CreateCategory();
    private static Category[] CreateCategory()
    {
        return
        [
            new Category
            {
                Id = 1,
                Name = "Beleza",
                IconCSS = "fas fa-spa"
            },
            new Category
            {
                Id = 2,
                Name = "Moveis",
                IconCSS = "fas fa-couch"
            },
            new Category
            {
                Id = 3,
                Name = "Eletronicos",
                IconCSS = "fas fa-headphones"
            },
            new Category
            {
                Id = 4,
                Name = "Calcados",
                IconCSS = "fas fa-shoe-prints"
            }
        ];
    }
}
