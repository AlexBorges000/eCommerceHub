using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface ICarrinhoCompraService
{
    Task<OperationResult<List<RequestGetCarrinhoItemDto>>> GetItens(int usuarioId);
    Task<OperationResult<RequestGetCarrinhoItemDto>> AdicionaItem(RequestCarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<OperationResult<RequestGetCarrinhoItemDto>> DeleteItem(int id);
    Task<OperationResult<RequestGetCarrinhoItemDto>> AtualizaQuantidade(RequestCarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto);
}
