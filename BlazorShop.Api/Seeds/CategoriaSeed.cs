using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlazorShop.Api.Seeds;

public class CategoriaSeed
{
    public static readonly Categoria[] Categoria = CriarCategoria();
    private static Categoria[] CriarCategoria()
    {
        return
        [
            new Categoria
            {
                Id = 1,
                Nome = "Beleza",
                IconCSS = "fas fa-spa"
            },
            new Categoria
            {
                Id = 2,
                Nome = "Moveis",
                IconCSS = "fas fa-couch"
            },
            new Categoria
            {
                Id = 3,
                Nome = "Eletronicos",
                IconCSS = "fas fa-headphones"
            },
            new Categoria
            {
                Id = 4,
                Nome = "Calcados",
                IconCSS = "fas fa-shoe-prints"
            }
        ];
    }
}
