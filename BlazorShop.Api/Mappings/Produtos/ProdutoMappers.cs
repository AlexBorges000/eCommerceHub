using BlazorShop.Models.DTOs.ProdutoDtos;
using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Mappings.Produtos;

public static class ProdutoMappers
{
    public static IEnumerable<RequestGetProdutoDto> ToDto(this IEnumerable<Produto> produtos)
    {
        return produtos.Select(p => p.ToDto());
    }

    public static RequestGetProdutoDto ToDto(this Produto produto)
    {
        return new RequestGetProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ImagemUrl = produto.ImagemUrl,
            Quantidade = produto.Quantidade,
            CategoriaId = produto.CategoriaId,
            CategoriaNome = produto.Categoria.Nome
        };
    }
}
