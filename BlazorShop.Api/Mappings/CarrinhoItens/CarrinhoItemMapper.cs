using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.CarrinhoDtos;

namespace BlazorShop.Api.Mappings.CarrinhoItens;

public static class CarrinhoItemMapper
{
    public static IEnumerable<RequestGetCarrinhoItemDto> ToDto(this IEnumerable<CarrinhoItem> carrinhoItens)
    {
        return carrinhoItens.Select(item => item.ToDto());
    }

    public static RequestGetCarrinhoItemDto ToDto(this CarrinhoItem carrinhoItem)
    {
        return new RequestGetCarrinhoItemDto
        {
            Id = carrinhoItem.Id,
            ProdutoId = carrinhoItem.ProdutoId,
            ProdutoNome = carrinhoItem.Produto.Nome,
            ProdutoDescricao = carrinhoItem.Produto.Descricao,
            ProdutoImagemURL = carrinhoItem.Produto.ImagemUrl,
            Preco = carrinhoItem.Produto.Preco,
            CarrinhoId = carrinhoItem.CarrinhoId,
            Quantidade = carrinhoItem.Quantidade,
            PrecoTotal = carrinhoItem.Produto.Preco * carrinhoItem.Quantidade
        };

    }
}
