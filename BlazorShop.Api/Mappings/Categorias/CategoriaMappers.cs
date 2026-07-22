using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.ProdutoDtos;

namespace BlazorShop.Api.Mappings.Categorias;

public static class CategoriaMappers
{
    public static IEnumerable<ResponseProdutoCategoriaDto> ToDto(this IEnumerable<Categoria> categorias)
    {
        return (from categoria in categorias
                select new ResponseProdutoCategoriaDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    IconCSS = categoria.IconCSS
                }).ToList();
    }
}
