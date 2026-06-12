using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services.Interfaces;

public interface ICarrinhoCompraService
{
    Task<OperationResult<List<CarrinhoItemDto>>> GetItens(int usuarioId);
    Task<OperationResult<CarrinhoItemDto>> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<OperationResult<CarrinhoItemDto>> DeleteItem(int id);
    Task<OperationResult<List<CarrinhoItemDto>>> GetItensReturnsResult(int usuarioId);
    Task<OperationResult<CarrinhoItemDto>> AtualizaQuantidade(int usuarioId, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto);
}
