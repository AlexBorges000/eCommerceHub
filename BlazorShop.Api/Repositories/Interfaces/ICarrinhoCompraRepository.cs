using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface ICarrinhoCompraRepository
{
    Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtulizaQuantidadeDto);
    Task<CarrinhoItem> DeleteItem(int id);
    Task<CarrinhoItem> GetItem(int id);
    Task<IEnumerable<CarrinhoItem>> GetItens(int usuarioId);
}